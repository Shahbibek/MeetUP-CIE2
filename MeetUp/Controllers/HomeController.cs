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
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            var data = GetFaculty();
            var chat = GetChatDetails(new GetChat());

            var viewModel = new AccessModel()
            {

                Faculty = data,               
                Chat = new GetChat()

            };

            //return View(viewModel);
            

                //List<Faculty> data = new List<Faculty>();
                //try
                //{
                //    using (HttpClient client = new HttpClient())
                //    {
                //        client.BaseAddress = new Uri(BaseUrl);
                //        client.DefaultRequestHeaders.Accept.Clear();
                //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //        HttpResponseMessage response = client.GetAsync("api/Authentication/FacultyDetails").Result;
                //        client.Dispose();
                //        if (response.IsSuccessStatusCode)
                //        {
                //            List<Faculty> list = new List<Faculty>();
                //            string stringData = response.Content.ReadAsStringAsync().Result;
                //            //var desdata = JsonConvert.DeserializeObject<Faculty>(stringData);
                //            data = JsonConvert.DeserializeObject<List<Faculty>>(stringData);

                //        }
                //        else
                //        {
                //            TempData["error"] = $"{response.ReasonPhrase}";
                //        }
                //    }


                //}
                //catch (Exception ex)
                //{
                //    TempData["Exception"] = ex.Message;
                //}
                return View(viewModel);
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

        //Get faculty list data fetch from api start
        private List<Faculty> GetFaculty()
        {

            List<Faculty> data = new List<Faculty>();
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
            return data;

        }

        //Get faculty list data fetch from api end

        //Get chat bot data fetch from api start
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<List<GetChat>> GetChatDetails(GetChat model)
        {
            List<GetChat> chat = new List<GetChat>();

            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        var data = JsonConvert.SerializeObject(model);
                        var contentData = new StringContent(data, Encoding.UTF8, "application/json");
                        var response = client.PostAsync("api/Authentication/ChatBot", contentData).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            chat = JsonConvert.DeserializeObject<List<GetChat>>(responseContent);
                        }
                        else
                        {
                            var errorMessage = await response.Content.ReadAsStringAsync();
                        }


                    }
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return  chat;
        }
        //Get chat bot data fetch from api end


        public IActionResult PopUpPartial()
        {
            return PartialView("_PopUpPartial", GetChatDetails);
        }
       
    }
}
