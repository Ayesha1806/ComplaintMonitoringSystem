using DataAccessLayer.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Contracts
{
    public interface IAuthenticationDataAccessLayer
    {
        public Task<string> Register(Register model);
        public Task<TokenResponse> Login(Login model);
    }
}
