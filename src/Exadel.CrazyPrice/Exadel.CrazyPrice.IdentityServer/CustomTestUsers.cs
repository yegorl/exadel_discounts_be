using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer.Models;

namespace Exadel.CrazyPrice.IdentityServer
{
    public static class CustomTestUsers
    {
        public static IEnumerable<CustomUser> Users =>
            new CustomUser[]
            {
                new CustomUser
                {
                    SubjectId = "97b248a1-c706-4987-9b31-101429ec2aff",
                    Username = "Frank",
                    Email = "Frank@gmail.com",
                    Password = "1111",
                    Role = "employee"
                },
                new CustomUser
                {
                    SubjectId = "13b4e6a3-42f2-4a43-be4e-7fc15454a7ab",
                    Username = "Claire",
                    Email = "Claire@gmail.com",
                    Password = "2222",
                    Role = "moderator"
                },
                new CustomUser
                {
                    SubjectId = "10c1a0a1-556a-475f-9597-c705dc1cc48b",
                    Username = "Bob",
                    Email = "Bob@gmail.com",
                    Password = "3333",
                    Role = "admin"
                }
            };
    }
}
