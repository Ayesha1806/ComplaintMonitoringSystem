using BusinessLogicLayer.Repository.Contracts;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddComplient(ComplientBox comp) => Ok(await _services.AddComplient(comp));
        [HttpGet("GetById")]
        public async Task<IActionResult> GetComplientList([FromQuery] string id) => Ok(await _services.RequestedByEmployee(id));

        [HttpGet("GetAllRecords")]
        public async Task<IActionResult> GetAllREcords() => Ok(await _services.GetAllComplients());

    }
}
