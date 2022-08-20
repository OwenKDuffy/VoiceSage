using Microsoft.AspNetCore.Mvc;

namespace VoiceSageExample.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
