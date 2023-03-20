using Microsoft.AspNetCore.Mvc;

namespace MeetUp.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
