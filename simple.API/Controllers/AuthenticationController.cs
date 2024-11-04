using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using simple.API.Domains;
using simple.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace simple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private AppDbContext _context;
        private IConfiguration _configuration;
        public AuthenticationController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private List<Account> Accounts = new List<Account> {
         new Account{ Username = "duonght", Password = "123456" },
         new Account{ Username = "admin", Password = "123456" },
        };


        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public AuthenResponse Login(AuthenRequest model)
        {
            try
            {
                var token = string.Empty;
                object resUser = new();
                object resRole = new();
                var success = false;
                var message = string.Empty;

                if (ModelState.IsValid)
                {
                    var account = Accounts.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
                    if (account != null)
                    {
                        //create claims details based on the user information
                        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("Username", account.Username)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpiryInDays"]));

                        var tokenDescriptor = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: expiry,
                            signingCredentials: signIn
                            );

                        token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                        success = true;
                        resUser = account;
                    }
                    else
                    {
                        message = "Username/ password không chính xác";
                    }
                }
                return new AuthenResponse { Success = success, Token = token, User = resUser, Message = message };
            }
            catch (Exception ex)
            {
                return new AuthenResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
