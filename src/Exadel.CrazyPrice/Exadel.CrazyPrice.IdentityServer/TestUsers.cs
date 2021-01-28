// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace Exadel.CrazyPrice.IdentityServer.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "c3e2ac9d-66d5-4872-8486-293cba3c6d61",
                Username = "Frank",
                Password = "1111",
                Claims = new List<Claim>
                {
                    new Claim("role", "employee")
                }
            },
            new TestUser
            {
                SubjectId = "7556abe5-c2cb-4321-87e6-3d96f7186001",
                Username = "Claire",
                Password = "2222",
                Claims = new List<Claim>
                {
                    new Claim("role", "moderator")
                }
            }
        };
    }
}