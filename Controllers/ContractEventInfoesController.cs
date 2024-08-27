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
    public class ContractEventInfoesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractEventInfoesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ContractEventInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractEventInfo>>> GetContractEventInfos()
        {
            return await _context.ContractEventInfos.ToListAsync();
        }

        // GET: api/ContractEventInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractEventInfo>> GetContractEventInfo(int id)
        {
            var contractEventInfo = await _context.ContractEventInfos.FindAsync(id);

            if (contractEventInfo == null)
            {
                return NotFound();
            }

            return contractEventInfo;
        }

        // PUT: api/ContractEventInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractEventInfo(int id, ContractEventInfo contractEventInfo)
        {
            if (id != contractEventInfo.ContractEventInfoId)
            {
                return BadRequest();
            }

            _context.Entry(contractEventInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractEventInfoExists(id))
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

        // POST: api/ContractEventInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("SaveEventInfo")]
        public async Task<ActionResult<ContractEventInfo>> PostContractEventInfo(ContractEventInfo contractEventInfo)
        {
            _context.ContractEventInfos.Add(contractEventInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractEventInfo", new { id = contractEventInfo.ContractEventInfoId }, contractEventInfo);
        }

        // DELETE: api/ContractEventInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractEventInfo(int id)
        {
            var contractEventInfo = await _context.ContractEventInfos.FindAsync(id);
            if (contractEventInfo == null)
            {
                return NotFound();
            }

            _context.ContractEventInfos.Remove(contractEventInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractEventInfoExists(int id)
        {
            return _context.ContractEventInfos.Any(e => e.ContractEventInfoId == id);
        }
    }
}
