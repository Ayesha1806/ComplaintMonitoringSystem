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
        [HttpPost("AddComplient")]
        [Authorize(Roles ="Employee")]
        public async Task<IActionResult> AddComplient(ComplientBox comp) => Ok(await _services.AddComplient(comp));
        [HttpGet("GetById")]
        [Authorize(Roles ="Employee")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetComplientList([FromQuery] string id) => Ok(await _services.RequestedByEmployee(id));

        [HttpGet("GetAllRecords")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllRecords() => Ok(await _services.GetAllComplients());
        [HttpGet("GetByComplientId")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> GetComplientByID(int id) => Ok(await _services.GetAllComplients());

    }
}
