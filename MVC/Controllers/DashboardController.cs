using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                string baseURL = "https://localhost:7152/";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); string strPayload = JsonConvert.SerializeObject(register);
                    HttpContent context = new StringContent(strPayload, Encoding.UTF8, "application/json");
                    var response = client.PostAsync("api/Authentication/Register", context).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return View("~/Home/Index");
                    }
                }
            }
            return View("~/Dashboard/Index");
        }

    }
}
