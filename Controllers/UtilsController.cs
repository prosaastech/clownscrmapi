using ClownsCRMAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.CustomModels;
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
 
        [HttpPost("SendToken")]
        public async Task<ActionResult<bool>> SendToken(int customerId, int contractId)
        {
            try
            {
                var cust = _context.CustomerInfos.Where(o => o.CustomerId == customerId).FirstOrDefault();
                if (cust != null)
                {
                    Extensions.SendTokenEmail(cust.EmailAddress, customerId, contractId);
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
