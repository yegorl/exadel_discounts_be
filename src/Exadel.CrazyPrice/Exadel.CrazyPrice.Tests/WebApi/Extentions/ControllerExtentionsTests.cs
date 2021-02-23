using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Extentions
{
    public class ControllerExtentionsTests
    {
        [Fact]
        public void GetUserIdTest()
        {
            var context = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(
                            new ClaimsIdentity(
                                new List<Claim>()
                                {
                                    new("sub", "f7211928-669e-4d40-b9e1-35b685945a04")
                                }
                        ))
                }
            };
            var guid = context.GetUserId();
            guid.Should().Be("f7211928-669e-4d40-b9e1-35b685945a04");
        }
    }
}
