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
