using Microsoft.AspNetCore.Mvc;
using MVC.Helper;
using MVC.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Controllers
{
    public class ComplaintController : Controller
    {
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
                var postTask = client.PostAsJsonAsync<ComplientBox>("api/Employee/AddEmployee", comp);
               // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
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
       
    }
}
