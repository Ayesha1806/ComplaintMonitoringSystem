using DataAccessLayer.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repository.Contracts
{
    public interface IAuthenticationBusinessLayer
    {
        Task<string> Register(Register model);
        Task<TokenResponse> Login(Login model);
    }
}
