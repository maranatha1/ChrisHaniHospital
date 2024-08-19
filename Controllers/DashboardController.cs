using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChrisHaniHospital.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Patients()
        {
            return View();
        }

        public IActionResult Pharmacist()
        {
            return View();
        }

        public IActionResult Surgeon()
        {
            return View();
        }

        public IActionResult Administrator()
        {
            return View();
        }

        public IActionResult Anaesthesiologist()
        {
            return View();
        }
    }
}
