using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}