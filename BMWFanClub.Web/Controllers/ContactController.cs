using Microsoft.AspNetCore.Mvc;

namespace BMWFanClub.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
