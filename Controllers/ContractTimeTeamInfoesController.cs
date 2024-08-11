using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.Models;

namespace ClownsCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTimeTeamInfoesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractTimeTeamInfoesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ContractTimeTeamInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractTimeTeamInfo>>> GetContractTimeTeamInfos()
        {
            return await _context.ContractTimeTeamInfos.ToListAsync();
        }

        // GET: api/ContractTimeTeamInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractTimeTeamInfo>> GetContractTimeTeamInfo(long id)
        {
            var contractTimeTeamInfo = await _context.ContractTimeTeamInfos.FindAsync(id);

            if (contractTimeTeamInfo == null)
            {
                return NotFound();
            }

            return contractTimeTeamInfo;
        }

        // PUT: api/ContractTimeTeamInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractTimeTeamInfo(long id, ContractTimeTeamInfo contractTimeTeamInfo)
        {
            if (id != contractTimeTeamInfo.ContractTimeTeamInfoId)
            {
                return BadRequest();
            }

            _context.Entry(contractTimeTeamInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractTimeTeamInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContractTimeTeamInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContractTimeTeamInfo>> PostContractTimeTeamInfo(ContractTimeTeamInfo contractTimeTeamInfo)
        {
            _context.ContractTimeTeamInfos.Add(contractTimeTeamInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractTimeTeamInfo", new { id = contractTimeTeamInfo.ContractTimeTeamInfoId }, contractTimeTeamInfo);
        }

        // DELETE: api/ContractTimeTeamInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractTimeTeamInfo(long id)
        {
            var contractTimeTeamInfo = await _context.ContractTimeTeamInfos.FindAsync(id);
            if (contractTimeTeamInfo == null)
            {
                return NotFound();
            }

            _context.ContractTimeTeamInfos.Remove(contractTimeTeamInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractTimeTeamInfoExists(long id)
        {
            return _context.ContractTimeTeamInfos.Any(e => e.ContractTimeTeamInfoId == id);
        }
    }
}
