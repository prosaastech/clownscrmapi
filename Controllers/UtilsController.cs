using ClownsCRMAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.CustomModels;
using NuGet.Common;
using Microsoft.Extensions.Caching.Memory;
using OtpNet;
using Microsoft.AspNetCore.Authorization;
namespace ClownsCRMAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UtilsController : ControllerBase
    {
        private readonly ClownsContext _context;

        public UtilsController(ClownsContext context)
        {
            _context = context;
        }
     

       
    
        [HttpPost("CancelContract")]
        public async Task<ActionResult<bool>> CancelContract(int customerId, int contractId)
        {
            try
            {
                List<ContractTimeTeamInfo> cust = await _context.ContractTimeTeamInfos.Where(o => o.CustomerId == customerId).ToListAsync();
                if (cust != null)
                {
                    foreach (var item in cust)
                    {
                        item.ContractStatusId = (int)Extensions.enumContractStatus.Cancelled;
                    }
                    await _context.SaveChangesAsync();
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
