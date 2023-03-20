using com.sun.net.ssl.@internal.www.protocol.https;
using com.sun.security.ntlm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using MVC.Helper;
using MVC.Models;
using Newtonsoft.Json;
using NServiceBus.Testing;
using System.ComponentModel;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;

namespace MVC.Controllers
{
    public class ComplaintController : Controller
    {
        //ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;  
        public static string bseurl = "https://localhost:7152/";
        ApiUrls _api = new ApiUrls();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetComplaints()
        {
            HttpClient client =_api.Initial();
            List<ComplientBox> complient = new List<ComplientBox>();
            try
            {
                var accessToken = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
                        JWT jwt = JsonConvert.DeserializeObject<JWT>(token);
                        if (jwt.Token == "Invalid credentials")
                        {
                            ViewBag.Message = "Incorrect UserID or Password!";
                            return Redirect("~/Home/Index");
                        }
                        HttpContext.Session.SetString("JWToken", jwt.Token);
                    }
                }
            }
            catch (Exception ex)
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
            catch (Exception e)
            {
                Ok(e.Message);
            }
            return Redirect("~/Home/Index");

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ComplientBox comp)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<ComplientBox>("api/Complient/RaiseComplient", comp);
            var result=postTask.Result;
            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetComplaints");
            }
            return View();
        }
            public async Task<IActionResult> Details(string id)
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
        public async Task<IActionResult> GetComplaintCount(string id)
        {
            HttpClient client = _api.Initial();
            Dictionary<string, int> complient = new Dictionary<string, int>();
            try
            {
                HttpResponseMessage res = await client.GetAsync("api/Complient/CountOfComplaints?id=" + id);
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    // complient = JsonConvert.DeserializeObject<Dictionary<string,int>>(result);
                    // ViewBag.result=result;
                    //complient.Add()
                    return View(complient);
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
        public async Task<IActionResult> EmployeeComplients()
        {
            HttpClient client = _api.Initial();
            List<ComplientBox> complient = new List<ComplientBox>();
            try
            {
                HttpResponseMessage res = await client.GetAsync("api/Complient/GetAllEmployess");
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
    }
}
