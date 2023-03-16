using ComplaintMonitoringSystemMVC.Helper;
using ComplaintMonitoringSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace ComplaintMonitoringSystemMVC.Controllers
{
    public class ComplaintController : Controller
    {
        ComplaintUrls _api=new ComplaintUrls();
        public async Task<IActionResult> Index()
        {
            List<ComplientBox> complaints = new List<ComplientBox>();
            HttpClient client = _api.Initial();
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage res = await client.GetAsync("api/Complient/GetAllRecords");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    complaints = JsonConvert.DeserializeObject<List<ComplientBox>>(result);
                }
            }
            catch(Exception ex)
            {
                Ok(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
            return View(complaints);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ComplientBox comp)
        {
            HttpClient client = _api.Initial();
            try
            {
                var accessToken = HttpContext.Session.GetString("JWToken");
                var postTask = client.PostAsJsonAsync<ComplientBox>("api/Complient/AddComplient", comp);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                postTask.Wait();
                var result = postTask.Result;

                RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Ok(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LoginUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(Login loginModel)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7152/api/Authentication/Login", stringContent))
                {
                    string token = await response.Content.ReadAsStringAsync();
                    JWT jwt = JsonConvert.DeserializeObject<JWT>(token);
                    if (jwt.Token == null)
                    {
                        ViewBag.Message = "Incorrect User or Password";
                        return Redirect("~/Home/Index");
                    }
                    HttpContext.Session.SetString("JWToken", jwt.Token);
                }
            }
            return Redirect("~/Complaint/Index");
        }

    }
}
