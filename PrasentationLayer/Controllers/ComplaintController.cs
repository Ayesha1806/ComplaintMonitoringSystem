using BusinessLogicLayer.Repository.Contracts;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace PrasentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintBusinessLayercs _services;
        public ComplaintController(IComplaintBusinessLayercs services)
        {
            _services = services;
        }
        [HttpPost("RaiseComplient")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> AddComplient(ComplientBox comp) => Ok(await _services.AddComplient(comp));
        [HttpGet("GetById")]
        [Authorize(Roles = "Employee")]
        
        public async Task<ActionResult> GetComplientList([FromQuery] string id) => Ok(await _services.RequestedByEmployee(id));

        [HttpGet("GetAllRecords")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllRecords() => Ok(await _services.GetAllComplients());
        [HttpGet("GetByComplientId")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetComplientByID(string id) => Ok(await _services.GetByComplientId(id));
        [HttpGet("CountOfComplaints")]
        public async Task<ActionResult> GetCount(string id)=>Ok(_services.NumberOfComplaintsRaised(id));
        [HttpGet("GetAllEmployess")]
        [Authorize(Roles ="Employee")]
        public async Task<ActionResult> GetAllEmployees()=>Ok(await _services.GetAllEmployees());
        [HttpGet("GetRecords")]
        public async Task<ActionResult> GetRecords()=>Ok(await _services.GetRecords());
    }
}
