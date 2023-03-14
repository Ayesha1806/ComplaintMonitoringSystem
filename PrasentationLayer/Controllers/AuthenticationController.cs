using BusinessLogicLayer.Repository.Contracts;
using DataAccessLayer.AuthenticationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrasentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusinessLayer _services;
        public AuthenticationController(IAuthenticationBusinessLayer services)
        {
            _services = services;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register model) => Ok(await _services.Register(model));
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login model) => Ok(await _services.Login(model));

    }
}
