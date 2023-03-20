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
    public class ComplientController : ControllerBase
    {
        private readonly IComplientBusinessLayercs _services;
        public ComplientController(IComplientBusinessLayercs services)
        {
            _services = services;
        }
        [HttpPost("RaiseComplient")]
        //[Authorize(Roles ="Employee")]
        public async Task<ActionResult<ComplientBox>> AddComplient(ComplientBox comp) => Ok(await _services.AddComplient(comp));
        [HttpGet("GetById")]
       // [Authorize(Roles ="Employee")]
        //[Authorize(Roles ="Admin")]
        public async Task<ActionResult> GetComplientList([FromQuery] string id) => Ok(await _services.RequestedByEmployee(id));

        [HttpGet("GetAllRecords")]
       // [Authorize(Roles ="Admin")]
        public async Task<ActionResult> GetAllRecords() => Ok(await _services.GetAllComplients());
        [HttpGet("GetByComplientId")]
       // [Authorize(Roles ="User")]
        public async Task<ActionResult> GetComplientByID(string id) => Ok(await _services.GetByComplientId(id));
        [HttpGet("CountOfComplaints")]
        public async Task<ActionResult> GetCount(string id)=>Ok(_services.NumberOfComplaintsRaised(id));
        [HttpGet("GetAllEmployess")]
        public async Task<ActionResult> GetAllEmployees()=>Ok(await _services.GetAllEmployees());

    }
}
