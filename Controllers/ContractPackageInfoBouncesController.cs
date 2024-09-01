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
    public class ContractPackageInfoBouncesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractPackageInfoBouncesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ContractPackageInfoBounces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractPackageInfoBounce>>> GetContractPackageInfoBounces()
        {
            return await _context.ContractPackageInfoBounces.ToListAsync();
        }

        // GET: api/ContractPackageInfoBounces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractPackageInfoBounce>> GetContractPackageInfoBounce(int id)
        {
            var contractPackageInfoBounce = await _context.ContractPackageInfoBounces.FindAsync(id);

            if (contractPackageInfoBounce == null)
            {
                return NotFound();
            }

            return contractPackageInfoBounce;
        }

        // PUT: api/ContractPackageInfoBounces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractPackageInfoBounce(int id, ContractPackageInfoBounce contractPackageInfoBounce)
        {
            if (id != contractPackageInfoBounce.ContractPackageInfoBounceId)
            {
                return BadRequest();
            }

            _context.Entry(contractPackageInfoBounce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractPackageInfoBounceExists(id))
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

        // POST: api/ContractPackageInfoBounces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContractPackageInfoBounce>> PostContractPackageInfoBounce(ContractPackageInfoBounce contractPackageInfoBounce)
        {
            _context.ContractPackageInfoBounces.Add(contractPackageInfoBounce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractPackageInfoBounce", new { id = contractPackageInfoBounce.ContractPackageInfoBounceId }, contractPackageInfoBounce);
        }

        // DELETE: api/ContractPackageInfoBounces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractPackageInfoBounce(int id)
        {
            var contractPackageInfoBounce = await _context.ContractPackageInfoBounces.FindAsync(id);
            if (contractPackageInfoBounce == null)
            {
                return NotFound();
            }

            _context.ContractPackageInfoBounces.Remove(contractPackageInfoBounce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractPackageInfoBounceExists(int id)
        {
            return _context.ContractPackageInfoBounces.Any(e => e.ContractPackageInfoBounceId == id);
        }
    }
}
