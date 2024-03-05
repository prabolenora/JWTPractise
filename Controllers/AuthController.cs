using Microsoft.AspNetCore.Mvc;

namespace JWTApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
