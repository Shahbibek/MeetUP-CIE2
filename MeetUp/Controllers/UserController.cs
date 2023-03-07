using Microsoft.AspNetCore.Mvc;

namespace MeetUp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Registration()
        {
            return View();
        }
    }
}
