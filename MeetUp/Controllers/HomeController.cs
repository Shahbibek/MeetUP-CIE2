using MeetUp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Newtonsoft.Json.Serialization;
using System.Data;
using System.Web.Helpers;
using Org.BouncyCastle.Ocsp;
using System.Collections;

namespace MeetUp.Controllers
{
    public class HomeController : Controller
    {
        private string BaseUrl = "http://shikatmahmud-001-site1.etempurl.com/";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Faculty> data = new List<Faculty>();
            //Faculty data = new Faculty();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Authentication/FacultyDetails").Result;
                    client.Dispose();
                    if (response.IsSuccessStatusCode)
                    {
                        List<Faculty> list = new List<Faculty>();
                        string stringData = response.Content.ReadAsStringAsync().Result;
                        //var desdata = JsonConvert.DeserializeObject<Faculty>(stringData);
                        data = JsonConvert.DeserializeObject<List<Faculty>>(stringData);
                       
                    }
                    else
                    {
                        TempData["error"] = $"{response.ReasonPhrase}";
                    }
                }


            }
            catch (Exception ex)
            {
                TempData["Exception"] = ex.Message;
            }
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}