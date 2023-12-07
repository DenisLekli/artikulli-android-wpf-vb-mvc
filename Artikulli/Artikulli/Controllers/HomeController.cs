using Microsoft.AspNetCore.Mvc;

namespace Artikulli.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
