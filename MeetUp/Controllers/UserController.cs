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
using System.Runtime.CompilerServices;
using MeetUp.Enums;
using MeetUp.Services;

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
                        HttpResponseMessage response = await client.PostAsync("api/Authentication/RegisterUser", RegisterData).ConfigureAwait(false);
                        //response.EnsureSuccessStatusCode();
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            //TempData["success"] = response.Content.ReadAsStringAsync().Result;
                            
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
            return RedirectToAction("Login", "User");

        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult ForgetPasswordLink()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetLogin(Login model)
        {
            List<UserDetails> dataList = new List<UserDetails>();
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var data = JsonConvert.SerializeObject(model);
                        var LoginData = new StringContent(data, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("api/Authentication/UserLogin", LoginData).ConfigureAwait(false);
                        //response.EnsureSuccessStatusCode();
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            //TempData["success"] = response.Content.ReadAsStringAsync().Result;
                            //string stringData = response.Content.ReadAsStringAsync().Result;
                            //var desdata = JsonConvert.DeserializeObject<Faculty>(stringData);
                            dataList = JsonConvert.DeserializeObject<List<UserDetails>>(responseContent);

                        }
                        else
                        {
                            TempData["error"] = response.Content.ReadAsStringAsync().Result;
                            ViewBag.Error = CommonServices.ShowAlert(Alerts.Danger, "Something went wrong !!");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "ModelState is not valid!!");
                    //return RedirectToAction("Login", "User");
                    return View("Login",model);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Home");
        }


        //public ActionResult Get()
        //{
        //    //Your other Code will go here 
        //    var session = HttpContext.Current.Session;
        //    Session["UserId"] = 
        //}
    }
}
