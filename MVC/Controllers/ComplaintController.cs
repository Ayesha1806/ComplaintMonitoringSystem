using Microsoft.AspNetCore.Mvc;
using MVC.Helper;
using MVC.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;

namespace MVC.Controllers
{
    public class ComplaintController : Controller
    {
        //public static string baseUrl=
        ApiUrls _api = new ApiUrls();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetComplaints()
        {
            HttpClient client = _api.Initial();
            List<ComplientBox> complient = new List<ComplientBox>();
            try
            {
                HttpResponseMessage res = await client.GetAsync("api/Complient/GetAllRecords");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    complient = JsonConvert.DeserializeObject<List<ComplientBox>>(result);
                }
            }
            catch (Exception ex)
            {
                Ok(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
            return View(complient);
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
                        return View("~/Complaint/Login");
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> LoginUser(Login user)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("https://localhost:7152/api/Authentication/Login", stringContent))
                    {
                        string token = await response.Content.ReadAsStringAsync();
                        if (token == "Invalid credentials")
                        {
                            ViewBag.Message = "Incorrect UserID or Password!";
                            return Redirect("~/Home/Index");
                        }
                        HttpContext.Session.SetString("JWToken", token);
                    }
                }
            }
            catch(Exception ex)
            {
                Ok(ex.Message);
            }
            return Redirect("~/Dashboard/Index");
        }
        public IActionResult Logoff()
        {
            try
            {
                HttpContext.Session.Clear();//clrear token
            }
            catch(Exception e)
            {
                Ok(e.Message);
            }
           return Redirect("~/Home/Index");

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ComplientBox comp)
        {
            string baseURL = "https://localhost:7152/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string strPayload = JsonConvert.SerializeObject(comp);
                HttpContent context = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var token = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.PostAsync("api/Complient/AddComplient", context).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllEmployees");
                }
                return View(comp);
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            HttpClient client = _api.Initial();
            var complaint = new ComplientBox();
            try
            {
                HttpResponseMessage res = await client.GetAsync("api/Complient/GetByComplientId?id=" + id);
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    complaint = JsonConvert.DeserializeObject<ComplientBox>(result);
                }
            }
            catch (Exception ex)
            {
                Ok(ex.Message);
            }
            finally
            {
                client.Dispose();
                //complaint = null;
            }
            return View(complaint);
        }
        public async Task<IActionResult> GetByEmployeeId(string id)
        {
            HttpClient client = _api.Initial();
            List<ComplientBox> complient = new List<ComplientBox>();
            try
            {
                HttpResponseMessage res = await client.GetAsync("api/Complient/GetById?id=" + id);
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    complient = JsonConvert.DeserializeObject<List<ComplientBox>>(result);
                }
            }
            catch (Exception ex)
            {
                Ok(ex.Message);
            }
            finally
            {
                client.Dispose();
                //complaint = null;
            }
            return View(complient);
        }

    }
}
