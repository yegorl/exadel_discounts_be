using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.IdentityServer.Extentions;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.UI;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exadel.CrazyPrice.IdentityServer.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class ExternalController : Controller
    {
        private readonly ILogger<ExternalController> _logger;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public ExternalController(
            IIdentityServerInteractionService interaction,
            IEventService events,
            ILogger<ExternalController> logger,
            IUserService userService,
            IUserRepository userRepository)
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
            _userService = userService;
            _userRepository = userRepository;

            _interaction = interaction;
            _logger = logger;
            _events = events;
        }

        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public IActionResult Challenge(string scheme, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

            // validate returnUrl - either it is a valid OIDC URL or back to a local page
            if (Url.IsLocalUrl(returnUrl) == false && _interaction.IsValidReturnUrl(returnUrl) == false)
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }
            
            // start challenge and roundtrip the return URL and scheme 
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(Callback)), 
                Items =
                {
                    { "returnUrl", returnUrl }, 
                    { "scheme", scheme },
                }
            };

            return Challenge(props, scheme);
            
        }
        
        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Callback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var externalClaims = result.Principal.Claims.Select(c => $"{c.Type}: {c.Value}");
                _logger.LogDebug("External claims: {@claims}.", externalClaims);
            }

            // retrieve return URL
            var returnUrl = result.Properties?.Items["returnUrl"] ?? "~/";

            _logger.LogTrace("Callback. Post processing of external authentication from URL: {returnUrl}.", returnUrl);

            var (user, provider, providerUserId) = await GetUserFromExternalProvider(result);

            if (user.IsEmpty())
            {
                _logger.LogWarning("Callback. Provider:{provider}. UserId: {providerUserId}  " +
                                       "Redirected to: {returnUrl}.", provider, providerUserId, returnUrl);
                return Redirect(returnUrl);
            }

            _logger.LogInformation("Callback. Found user: id {id}, name {name}, surname {surname}, mail {mail}.",
                user.Id, user.Name, user.Surname, user.Mail);

            var additionalLocalClaims = new List<Claim>();
            var localSignInProps = new AuthenticationProperties();
            ProcessLoginCallback(result, additionalLocalClaims, localSignInProps);
            
            // issue authentication cookie for user
            var issuer = new IdentityServerUser(user.Id.ToString())
            {
                DisplayName = user.Name,
                IdentityProvider = provider.ToString(),
                AdditionalClaims = additionalLocalClaims
            };

            _logger.LogInformation("Callback. Provider:{provider}. UserId: {providerUserId}. " +
                                   "Issue authentication cookie for user: {@issuer}.", provider, providerUserId, issuer);

            await HttpContext.SignInAsync(issuer, localSignInProps);

            // delete temporary cookie used during external authentication
            var cookie = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            await HttpContext.SignOutAsync(cookie);

            _logger.LogInformation("Callback. Provider:{provider}. UserId: {providerUserId}. " +
                                   "Deleted temporary cookie: {cookie}.",provider, providerUserId, cookie);

            // check if external login is in the context of an OIDC request
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            await _events.RaiseAsync(new UserLoginSuccessEvent(provider.ToString(), providerUserId, user.Id.ToString(), 
                user.Name, true, context?.Client.ClientId));

            if (context != null)
            {
                if (context.IsNativeClient())
                {
                    _logger.LogInformation("Callback. Provider:{provider}. UserId: {providerUserId}. Redirected to native client: {returnUrl}.",
                        provider, providerUserId, returnUrl);

                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage("Redirect", returnUrl);
                }
            }

            _logger.LogInformation("Callback. Provider:{provider}. UserId: {providerUserId}. Redirected to: {returnUrl}.", 
                provider, providerUserId, returnUrl);

            return Redirect(returnUrl);
        }

        private async Task<(User user, ProviderOption provider, string providerId)> GetUserFromExternalProvider(AuthenticateResult result)
        {
            var claimsPrincipal = result.Principal;

            // try to determine the unique id of the external user (issued by the provider)
            // the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = claimsPrincipal.FindFirst(JwtClaimTypes.Subject) ??
                              claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier) ??
                              throw new Exception("Unknown userid");

            var claims = claimsPrincipal.Claims.ToList();

            var provider = Enum.Parse<ProviderOption>(result.Properties.Items["scheme"]);
            var providerUserId = userIdClaim.Value;

            var externalUser = new ExternalUser
            {
                Identifier = providerUserId,
                Provider = provider
            };

            _logger.LogInformation("Callback. Getting user. Incoming external user: {@externalUser}.", externalUser);

            // find external user
            var user = await _userRepository.GetUserByExternalUserAsync(externalUser);

            if (user.IsEmpty())
            {
                _logger.LogWarning("Callback. Getting user. External user not found: {@externalUser}.", externalUser);

                var mail = claims.GetClaimValue(ClaimTypes.Email);
                user = await _userRepository.GetUserByEmailAsync(mail);

                if (!user.IsEmpty())
                {
                    _logger.LogInformation("Callback. Getting user. Internal user with the same mail {mail} is exist. " +
                                           "External user: {@externalUser}", mail, externalUser);
                    await _userRepository.AddExternalUserIntoUserAsync(externalUser, user.Id);
                    _logger.LogInformation("Callback. Getting user. Added external user {@externalUser} into internal user {id}.",
                        externalUser, user.Id);
                }
                else
                {
                    _logger.LogWarning("Callback. Getting user. Internal user with the same mail {mail} is not exist. " +
                                       "External user: {@externalUser}.", mail, externalUser);

                    if (_userService.TryCreateUser(claims, provider, out user))
                    {
                        await _userRepository.AddUserAsync(user);
                        _logger.LogInformation("Callback. Getting user. Attempting to create a new user from an allowed pool " +
                                               "was successful. User: {@user}.", user);
                    }
                    else
                    {
                        _logger.LogWarning("Callback. Getting user. User was not created. Not allowed mail: {mail}. " +
                                           "External user: {@externalUser}", mail, externalUser);
                    }
                }
            }

            return (user, provider, providerUserId);
        }


        // if the external login is OIDC-based, there are certain things we need to preserve to make logout work
        // this will be different for WS-Fed, SAML2p or other protocols
        private void ProcessLoginCallback(AuthenticateResult externalResult, List<Claim> localClaims, AuthenticationProperties localSignInProps)
        {
            // if the external system sent a session id claim, copy it over
            // so we can use it for single sign-out
            var sid = externalResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                localClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            // if the external provider issued an id_token, we'll keep it for signout
            var idToken = externalResult.Properties.GetTokenValue("id_token");
            if (idToken != null)
            {
                localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
            }
        }
    }
}