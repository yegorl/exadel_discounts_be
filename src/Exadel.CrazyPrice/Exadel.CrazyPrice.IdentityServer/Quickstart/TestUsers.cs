// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "c3e2ac9d-66d5-4872-8486-293cba3c6d61",
                Username = "Frank",
                Password = "password1",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "Frank"),
                    new Claim("family_name", "Underwood"),
                    new Claim("role", "employee")
                }
            },
            new TestUser
            {
                SubjectId = "7556abe5-c2cb-4321-87e6-3d96f7186001",
                Username = "Claire",
                Password = "password2",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "Claire"),
                    new Claim("family_name", "Underwood"),
                    new Claim("role", "moderator")
                }
            }
        };
    }
}