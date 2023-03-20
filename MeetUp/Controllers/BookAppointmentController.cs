using Microsoft.AspNetCore.Mvc;

namespace MeetUp.Controllers
{
    public class BookAppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
