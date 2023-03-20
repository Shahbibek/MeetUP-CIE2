using Microsoft.AspNetCore.Mvc;

namespace MeetUp.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
