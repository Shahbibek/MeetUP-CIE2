using MeetUp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Newtonsoft.Json.Serialization;
using MeetUp.Interfaces;
using System.Text;
using System.Net.Http.Headers;

namespace MeetUp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        //private readonly IApiConnection _apiConnection;
        //IApiConnection apiConnection
        private String BaseUrl = "http://shikatmahmud-001-site1.etempurl.com/";
 

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            //_apiConnection = apiConnection;
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
            Users user = new Users();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Users model)
        {
            try
            {
                if (ModelState.IsValid)
                {   
                    using(var client=new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var data = JsonConvert.SerializeObject(model);
                        var RegisterData = new StringContent(data, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("api/Authentication/RegisterUser", RegisterData);
                        if (response.IsSuccessStatusCode)
                        {
                            TempData["success"] = response.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            TempData["error"] = response.Content.ReadAsStringAsync().Result;  
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "ModelState is not valid!!");
                    return View(model);
                }
            }
            catch (Exception )
            {
                throw;
            }
            return RedirectToAction("Login");

        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult ForgetPasswordLink()
        {
            return View();
        }

    }
}
