using ComplaintMonitoringSystemMVC.Helper;
using ComplaintMonitoringSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace ComplaintMonitoringSystemMVC.Controllers
{
    public class ComplaintController : Controller
    {
        ComplaintUrls _api=new ComplaintUrls();
        public async Task<IActionResult> Index()
        {
            List<ComplientBox> complaints=new List<ComplientBox>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Complient/GetAllRecords");
            if(res.IsSuccessStatusCode)
            {
                var result=res.Content.ReadAsStringAsync().Result;
                complaints=JsonConvert.DeserializeObject<List<ComplientBox>>(result);
            }
            return View(complaints);
        }
        
    }
}
