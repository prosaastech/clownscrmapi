using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClownsCRMAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContractPackageInfoAddonsController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractPackageInfoAddonsController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ContractPackageInfoAddons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractPackageInfoAddon>>> GetContractPackageInfoAddons()
        {
            return await _context.ContractPackageInfoAddons.ToListAsync();
        }

        // GET: api/ContractPackageInfoAddons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractPackageInfoAddon>> GetContractPackageInfoAddon(int id)
        {
            var contractPackageInfoAddon = await _context.ContractPackageInfoAddons.FindAsync(id);

            if (contractPackageInfoAddon == null)
            {
                return NotFound();
            }

            return contractPackageInfoAddon;
        }

        // PUT: api/ContractPackageInfoAddons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractPackageInfoAddon(int id, ContractPackageInfoAddon contractPackageInfoAddon)
        {
            if (id != contractPackageInfoAddon.ContractPackageInfoAddonId)
            {
                return BadRequest();
            }

            _context.Entry(contractPackageInfoAddon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractPackageInfoAddonExists(id))
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

        // POST: api/ContractPackageInfoAddons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContractPackageInfoAddon>> PostContractPackageInfoAddon(ContractPackageInfoAddon contractPackageInfoAddon)
        {
            _context.ContractPackageInfoAddons.Add(contractPackageInfoAddon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractPackageInfoAddon", new { id = contractPackageInfoAddon.ContractPackageInfoAddonId }, contractPackageInfoAddon);
        }

        // DELETE: api/ContractPackageInfoAddons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractPackageInfoAddon(int id)
        {
            var contractPackageInfoAddon = await _context.ContractPackageInfoAddons.FindAsync(id);
            if (contractPackageInfoAddon == null)
            {
                return NotFound();
            }

            _context.ContractPackageInfoAddons.Remove(contractPackageInfoAddon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractPackageInfoAddonExists(int id)
        {
            return _context.ContractPackageInfoAddons.Any(e => e.ContractPackageInfoAddonId == id);
        }
    }
}
