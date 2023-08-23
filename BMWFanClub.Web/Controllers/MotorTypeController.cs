using Microsoft.AspNetCore.Mvc;

namespace BMWFanClub.Web.Controllers
{
    public class MotorTypeController : Controller
    {
        public IActionResult Gasoline()
        {
            return View();
        }

        public IActionResult Diesel()
        {
            return View();
        }
    }
}
