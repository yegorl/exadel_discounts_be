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
                    Password = "W1BKFpkamZaLjBiULqea/DQTDNa1yCa+|8312|qMys3s2rN6+pLboyOOUxvdeNd1TjiNEH",
                    Role = "employee"
                },
                new CustomUser
                {
                    SubjectId = "13b4e6a3-42f2-4a43-be4e-7fc15454a7ab",
                    Username = "Claire",
                    Email = "Claire@gmail.com",
                    Password = "x19Vo3PvwSCarSwya+fde2EcmeXGmPlI|8312|rC4jenVp7jGG2X+uHcR7mweVyG6+pTsT",
                    Role = "moderator"
                },
                new CustomUser
                {
                    SubjectId = "10c1a0a1-556a-475f-9597-c705dc1cc48b",
                    Username = "Bob",
                    Email = "Bob@gmail.com",
                    Password = "+MQ8pPkNkxj6WoC0Yac2QVb149z39wc3|8312|pYALJHKH92W5zKchwQ5jDaFP8WIuZI9t",
                    Role = "admin"
                }
            };
    }
}
