using Microsoft.AspNetCore.Mvc;

namespace Pronia.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
