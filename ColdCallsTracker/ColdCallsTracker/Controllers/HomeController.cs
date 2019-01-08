using Microsoft.AspNetCore.Mvc;

namespace ColdCallsTracker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
