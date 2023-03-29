using DataAccessLayer.AuthenticationModels;
using DataAccessLayer.Repository.Contracts;
using GlobalEntity.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Services
{
    public class AuthenticationServicesDataAccessLayer :IAuthenticationDataAccessLayer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationServicesDataAccessLayer> _logger;
        public AuthenticationServicesDataAccessLayer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger<AuthenticationServicesDataAccessLayer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> Register(Register model)
        {
            try
            {
                _logger.LogInformation("User Register");
                _logger.LogDebug(model.Email, model.Username, model.Password);
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                {
                    return "User Already Exists";
                }
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return null;
                }
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await _roleManager.RoleExistsAsync(UserRoles.Employee))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));
                switch (model.Role)
                {
                    case "Admin":
                        await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                        break;
                    case "User":
                        await _userManager.AddToRoleAsync(user, UserRoles.User);
                        break;
                    case "Employee":
                        await _userManager.AddToRoleAsync(user, UserRoles.Employee);
                        break;
                    default:
                        await _userManager.AddToRoleAsync(user, UserRoles.User);
                        break;
                }
                return "User Created Sucessfully";
            }
            catch (ServerError )
            {
                _logger.LogError("Somwthing Went Wrong");
                throw new ServerError("Something Went Wrong!!!!");
            }
        }
        public async Task<TokenResponse> Login(Login model)
        {
            try
            {
                _logger.LogInformation("User Register");
                _logger.LogDebug(model.Username, model.Password);
                var  user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                     new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var token = GetToken(authClaims);
                    TokenResponse tokenResponse = new TokenResponse();
                    tokenResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    tokenResponse.Expire = token.ValidTo;
                    return tokenResponse;
                }
                return null;
            }
            catch (BadRequest)
            {
                _logger.LogError("Something Went Wrong");
                throw new BadRequest("Something Went Wrong!!!");
            }
        }
        
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            try
            {
                _logger.LogInformation("User Register");
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken
                    (
                      issuer: _configuration["JWT:ValidIssuer"],
                      audience: _configuration["JWT:ValidAudience"],
                      expires: DateTime.Now.AddHours(3),
                      claims: authClaims,
                      signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return token;
            }
            catch (BadRequest)
            {
                _logger.LogError("Something Went Wrong");
                throw new BadRequest("Something Went Wrong!!!");
            }
        }
    }
}
