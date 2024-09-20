using ClownsCRMAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.CustomModels;
using NuGet.Common;
using Microsoft.Extensions.Caching.Memory;
using OtpNet;
namespace ClownsCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilsController : ControllerBase
    {
        private readonly ClownsContext _context;

        public UtilsController(ClownsContext context)
        {
            _context = context;
        }
        private const string SecretKey = "Ht43mkcsrmvSSd";

        public static string GenerateToken()
        {
            var totp = new Totp(Base32Encoding.ToBytes(SecretKey));
            return totp.ComputeTotp();
        }

        [HttpPost("ValidateToken")]
        public async Task<ActionResult<bool>> ValidateToken(int customerId, string token, [FromServices] IMemoryCache cache)
        {
            // Retrieve the token from the cache
            if (cache.TryGetValue($"Token_{customerId}", out string cachedToken) && cachedToken == token)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
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
    }
}
