using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.IdentityServer
{
    public static class CustomTestUsers
    {
        public static IEnumerable<User> Users =>
            new User[]
            {
                new User
                {
                    Id = new Guid("97b248a1-c706-4987-9b31-101429ec2aff"),
                    Name = "Frank",
                    Mail = "Frank@gmail.com",
                    HashPassword = "+SzcAYCGZcD+eov22e4GpybKoLsaoguAIPS0D3Ntj2ATCpcQvvbsitFLFT1IZOTEzjvxTOQ/ISdxSW9x+VmtV1UFTGgIgKPZPKMtkWmyL7SOza6iuFbikXRr6olGNkdRK6KEOSyY0d+S1VZ9RbcGl9eYfppPM5s8xVc7w0FsODw=",
                    Salt = "IZmh6o3UgDIHs5rXD3L5hD2momUnLQdyJUrpQNW/",
                    Roles = RoleOption.Employee
                },
                new User
                {
                    Id = new Guid("13b4e6a3-42f2-4a43-be4e-7fc15454a7ab"),
                    Name = "Claire",
                    Mail = "Claire@gmail.com",
                    HashPassword = "+fs8G3LGPEg3HFHyJNfv4jAe+a9zKCj4AgZDBVatsGcHW625Ce+QO6cTcI/oLFn2ArUvvYPs7Hs664OPojKm3A6kPAQ77ysqzigxg75xbKS2Cel5Un7BaIBMN+BFRU5CnaXnPQ4rmOENf8p70FbdwWr359pvmTttFWsQAG2iCjI=",
                    Salt = "sOVqvk4NWiJ4gbTeVJ19M3V2gG5jOdootBQbTv6v",
                    Roles = RoleOption.Moderator | RoleOption.Employee
                },
                new User
                {
                    Id = new Guid("10c1a0a1-556a-475f-9597-c705dc1cc48b"),
                    Name = "Bob",
                    Mail = "Bob@gmail.com",
                    HashPassword = "voWfJR7QxiT3sTuLZMu+iuYswOika7FU+VTtRhATkhSdzznn7pOnSH1VEsZbNlqWOaRpvTskIlBUmvXwct5KZ94cg3T93dVLmSCenh8VjPmFKTiGHxP+dboJLXjeKQ6BUNoNwn3w5v16OFRH+QgGesrdkWsLi2V5QQ/+BO9VDVA=",
                    Salt = "wQFhWPjNcYukdImNixjiATqeQLemqaJA55jQkfgg",
                    Roles = RoleOption.Administrator | RoleOption.Moderator | RoleOption.Employee
                }
            };
    }
}
