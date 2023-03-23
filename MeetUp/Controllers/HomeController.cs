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
using System;

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
            var Chatdata = GetChat();

            var viewModel = new AccessModel()
            {

                Faculty = data,
                GetChatDetails = Chatdata

            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // second time popup partial view start
        public async Task<IActionResult> PopUpPartial()
        {
            var Chatdata = GetChat();
            var data = GetFaculty();
            var viewModel = new AccessModel()
            {
                Faculty = data,
                GetChatDetails = Chatdata
                //Chat = new GetChat()

            };
            return PartialView("_PopUpPartial", viewModel);
            //return View(viewModel);
            //return Content("success");
        }

        // second time popup partial view end

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

        //send the search data to api start

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendChatRequest(GetChat chat)
        {
            GetResponse message = new GetResponse();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    var data1 = chat;
                    var data = JsonConvert.SerializeObject(data1);
                    var contentData = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = client.PostAsync("api/Authentication/ChatBot", contentData).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);
                    }
                    else
                    {
                        var errorMessage = response.Content.ReadAsStringAsync();
                    }

                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            //return message;
            return RedirectToAction("PopUpPartial");
            //return Content("success");
        }

        //[HttpPost]
        //public async Task<ContentResult> FormChat(GetChat chat)
        //{
        //    try
        //    {
        //        var ChatSend = SendChatRequest();

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //    return Content("success");
        //}
        //send the search data to api end

        //Get chat details from the api start (response)
        private List<GetChatDetails> GetChat()
        {

            List<GetChatDetails> Chatdata = new List<GetChatDetails>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Authentication/GetChat").Result;
                    client.Dispose();
                    if (response.IsSuccessStatusCode)
                    {
                        List<GetChatDetails> list = new List<GetChatDetails>();
                        string stringData = response.Content.ReadAsStringAsync().Result;
                        if (stringData != null)
                        {
                            Chatdata = JsonConvert.DeserializeObject<List<GetChatDetails>>(stringData);
                        }
                        //var desdata = JsonConvert.DeserializeObject<Faculty>(stringData);

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
            return Chatdata;

        }

        //Get chat details from the api end (response)
    }
}
