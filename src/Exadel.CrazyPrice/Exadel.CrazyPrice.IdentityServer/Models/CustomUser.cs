using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Exadel.CrazyPrice.IdentityServer.Models
{
    public class CustomUser
    {
        public Guid SubjectUid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
    }
}
