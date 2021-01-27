﻿using Exadel.CrazyPrice.TestClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Exadel.CrazyPrice.TestClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [Authorize(Roles = "moderator, employee")]
        public async Task<IActionResult> Index()
        {
            await WriteOutIdentityInformation();

            var httpClient = _httpClientFactory.CreateClient("CrazyPriceAPI");
            var request = new HttpRequestMessage(
                HttpMethod.Get, "/tags/");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                return View();
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden ||
                      response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }

            throw new Exception("Problem accessing the API");
            
            
            //using (var responseStream = await  response.Content.ReadAsStreamAsync())
            //{
            //    ViewBag.Message = responseStream.;
            //    return View();
            //}
        }

        public async Task Logout()
        {

            var client = _httpClientFactory.CreateClient("IdentityServer");

            var discoveryDocumentResponse = await client.GetDiscoveryDocumentAsync();
            if (discoveryDocumentResponse.IsError)
            {
                throw new Exception(discoveryDocumentResponse.Error);
            }

            var accessTokenRevocationResponse = await client.RevokeTokenAsync(
                new TokenRevocationRequest
                {
                    Address = discoveryDocumentResponse.RevocationEndpoint,
                    ClientId = "crazypricetestclient",
                    ClientSecret = "secret",
                    Token = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken)
                });

            if (accessTokenRevocationResponse.IsError)
            {
                throw new Exception(accessTokenRevocationResponse.Error);
            }

            var refreshTokenRevocationResponse = await client.RevokeTokenAsync(
                new TokenRevocationRequest
                {
                    Address = discoveryDocumentResponse.RevocationEndpoint,
                    ClientId = "crazypriceclient",
                    ClientSecret = "secret",
                    Token = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken)
                });

            if (refreshTokenRevocationResponse.IsError)
            {
                throw new Exception(accessTokenRevocationResponse.Error);
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
        
        [Authorize(Roles = "moderator")]
        public async Task<IActionResult> Exclusive()
        {
            var httpClient = _httpClientFactory.CreateClient("CrazyPriceAPI");
            var request = new HttpRequestMessage(
                HttpMethod.Get, "/tags/");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var message = response.Content.ReadAsStringAsync().Result;
                return Ok(message);
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden ||
                     response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }

            throw new Exception("Problem accessing the API");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //for testing
        public async Task WriteOutIdentityInformation()
        {
            //get the saved identity token
            var identityToken = await HttpContext.
                GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity token: {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }
    }
}
