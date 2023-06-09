﻿using com.sun.net.ssl.@internal.www.protocol.https;
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
                HttpResponseMessage res = await client.GetAsync("api/Complaint/GetAllRecords");
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
                        
                        if (jwt != null)
                        {
                            //if (jwt.Role == "Admin")
                            //{
                            //    HttpContext.Session.SetString("JWToken", jwt.Token);
                            //    return Redirect("~/Dashboard/Index");
                            //}
                            //else if(jwt.Role=="Employee")
                            //{
                            //    HttpContext.Session.SetString("JWToken", jwt.Token);
                            //    return Redirect("~/Dashboard/Index1");
                            //}
                            if (jwt.Role == "Admin")
                            {
                                HttpContext.Session.SetString("JWToken", jwt.Token);
                                TempData["Role"] = "Admin";
                                return Redirect("~/Dashboard/Index");
                            }
                            if (jwt.Role == "Employee")
                            {
                                HttpContext.Session.SetString("JWToken", jwt.Token);
                                TempData["Role"] = null;
                                return Redirect("~/Dashboard/Index");
                            }

                        }
                        ViewData["LoginFlag"] = "Invalid user name or password";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                Ok(ex.Message);
            }
            return View();
            // return Redirect("~/Home/Index");
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
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string baseURL = "https://localhost:7152/";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseURL);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string strPayload = JsonConvert.SerializeObject(register);
                        HttpContent context = new StringContent(strPayload, Encoding.UTF8, "application/json");
                        var response = client.PostAsync("api/Authentication/Register", context).Result;
                        if (response!=null && response.StatusCode.ToString()=="OK")
                        {
                            TempData["AlertMessage"] = "User Created Sucessfully";
                            return Redirect("Register");
                        }
                        ViewData["LoginFlag"] = "Invalid Entries!!!Please check the details.";
                        return View();
                    }
                    return View(register);
                }
                catch (Exception ex)
                {
                    Ok(ex.Message);
                }
            }
            return View();
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
                ComplientBox complientBoxcomp = new ComplientBox()
                {
                    Issue = comp.Issue,
                    EmployeeId = comp.EmployeeId,
                    ActiveFlag = true,
                    ComplientId = "123",
                    ComplientRaised = 0,
                    CreatedBy = "Ayesha",
                    CreatedDate = DateTime.Now,
                    Status = "Submited",
                    Resolution = "UnderProcessing"
                };
                var accessToken = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var postTask = client.PostAsJsonAsync<ComplientBox>("api/Complaint/RaiseComplient", complientBoxcomp);
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("EmployeeComplients");
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
            return View();
        }
        public async Task<IActionResult> Details(string id)
        {
            HttpClient client = _api.Initial();
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var complaint = new ComplientBox();
            try
            {
                HttpResponseMessage res = await client.GetAsync("api/Complaint/GetByComplientId?id=" + id);
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
            }
            return View(complaint);
        }
        public async Task<IActionResult> GetByEmployeeId(string id)
        {
            HttpClient client = _api.Initial();
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            List<ComplientBox> complient = new List<ComplientBox>();
            try
            {
                HttpResponseMessage res = await client.GetAsync("api/Complaint/GetById?id=" + id);
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
       
        public async Task<IActionResult> EmployeeComplients()
        {
            HttpClient client = _api.Initial();
            var accessToken = HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            List<ComplientBox> complient = new List<ComplientBox>();
            try
            {
                HttpResponseMessage res = await client.GetAsync("api/Complaint/GetAllEmployess");
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
