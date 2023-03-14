using BusinessLogicLayer.Repository.Contracts;
using DataAccessLayer.AuthenticationModels;
using DataAccessLayer.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repository.Services
{
    public class AuthenticationServicesBusinessLayer :IAuthenticationBusinessLayer
    {
        private readonly IAuthenticationDataAccessLayer _services;
        public AuthenticationServicesBusinessLayer(IAuthenticationDataAccessLayer services)
        {
            _services = services;
        }
        public Task<TokenResponse> Login(Login model)
        {
            return _services.Login(model);
        }

        public Task<string> Register(Register model)
        {
            return _services.Register(model);
        }

    }
}
