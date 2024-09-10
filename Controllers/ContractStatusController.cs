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
    public class ContractStatusController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractStatusController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ContractStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractStatus>>> GetContractStatuses()
        {
            return await _context.ContractStatuses.ToListAsync();
        }

        // GET: api/ContractStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractStatus>> GetContractStatus(int id)
        {
            var contractStatus = await _context.ContractStatuses.FindAsync(id);

            if (contractStatus == null)
            {
                return NotFound();
            }

            return contractStatus;
        }

        // PUT: api/ContractStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractStatus(int id, ContractStatus contractStatus)
        {
            if (id != contractStatus.ContractStatusId)
            {
                return BadRequest();
            }

            _context.Entry(contractStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractStatusExists(id))
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

        // POST: api/ContractStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContractStatus>> PostContractStatus(ContractStatus contractStatus)
        {
            _context.ContractStatuses.Add(contractStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractStatus", new { id = contractStatus.ContractStatusId }, contractStatus);
        }

        // DELETE: api/ContractStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractStatus(int id)
        {
            var contractStatus = await _context.ContractStatuses.FindAsync(id);
            if (contractStatus == null)
            {
                return NotFound();
            }

            _context.ContractStatuses.Remove(contractStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractStatusExists(int id)
        {
            return _context.ContractStatuses.Any(e => e.ContractStatusId == id);
        }
    }
}
