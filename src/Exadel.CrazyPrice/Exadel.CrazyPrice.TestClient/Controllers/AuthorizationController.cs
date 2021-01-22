using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.CrazyPrice.TestClient.Controllers
{
    public class AuthorizationController : Controller
    {
        public AuthorizationController()
        {
            
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
