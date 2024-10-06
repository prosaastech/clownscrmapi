using ClownsCRMAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.CustomModels;
using NuGet.Common;
using Microsoft.Extensions.Caching.Memory;
using OtpNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClownsCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilFuncController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ClownsContext _context;

        public UtilFuncController(ClownsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private const string SecretKey = "Ht43mkcsrmvSSd";


        public static string GenerateToken()
        {
            var totp = new Totp(Base32Encoding.ToBytes(SecretKey));
            return totp.ComputeTotp();
        }

        [HttpPost("SendToken")]
        public async Task<ActionResult<bool>> SendToken(int customerId, int contractId, [FromServices] IMemoryCache cache)
        {
            try
            {
                var cust = _context.CustomerInfos.Where(o => o.CustomerId == customerId).FirstOrDefault();
                if (cust != null)
                {
                    string Token = GenerateToken();


                    cache.Set($"Token_{customerId}", Token, TimeSpan.FromHours(1));

                    Extensions.SendTokenEmail(cust.EmailAddress, customerId, contractId, Token);
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        [HttpPost("ValidateToken")]
        public async Task<ActionResult<bool>> ValidateToken(int customerId, string token, [FromServices] IMemoryCache cache)
        {
            // Retrieve the token from the cache
            if (cache.TryGetValue($"Token_{customerId}", out string cachedToken) && cachedToken == token)
            {

                var loginUser= _context.CustomerInfos.Where(o => o.CustomerId == customerId).FirstOrDefault();


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
            new Claim(ClaimTypes.Name, loginUser.FirstName),
            new Claim(ClaimTypes.Email, loginUser.EmailAddress),
            new Claim("branchId", loginUser.BranchId.ToString()), // Add BranchId claim
            new Claim("companyId", loginUser.CompanyId.ToString()) // Add CompanyId claim
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Issuer"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token2 = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token2);

                return Ok(new { Token = tokenString });
                 
            }
            else
            {
                return Ok(false);
            }
        }
    }

}
