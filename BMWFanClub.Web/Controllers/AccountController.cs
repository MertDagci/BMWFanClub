using Microsoft.AspNetCore.Mvc;

namespace BMWFanClub.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {

            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
