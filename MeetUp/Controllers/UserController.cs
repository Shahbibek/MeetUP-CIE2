using MeetUp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Newtonsoft.Json.Serialization;

namespace MeetUp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        String BaseURL = "http://shikatmahmud-001-site1.etempurl.com/api/Authentication/";

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        
        public async Task<ActionResult<string>> UserRegister(Users users)
        {
            //IList<Users> user = new List<Users>();
            Users obj = new Users()
            {
                Email = users.Email,
                FName = users.FName,
                LName = users.LName,
                Password = users.Password,
                CNFPassword = users.Password,

            };
            if(users.Email != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Result = await client.PostAsJsonAsync <Users>("RegisterUser",obj);
                    if (Result.IsSuccessStatusCode)
                    {
                        return Ok(Result);
                    }
                    else
                    {
                        Console.WriteLine("Error calling web Api");
                    }

                }
                
            }

            return View();

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }

    }
}
