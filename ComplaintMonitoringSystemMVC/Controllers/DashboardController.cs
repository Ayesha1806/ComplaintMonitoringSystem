using Microsoft.AspNetCore.Mvc;

namespace ComplaintMonitoringSystemMVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
