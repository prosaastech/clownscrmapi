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
    public class PartyPackagesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public PartyPackagesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/PartyPackages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartyPackage>>> GetPartyPackages()
        {
            return await _context.PartyPackages.ToListAsync();
        }

        // GET: api/PartyPackages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartyPackage>> GetPartyPackage(int id)
        {
            var partyPackage = await _context.PartyPackages.FindAsync(id);

            if (partyPackage == null)
            {
                return NotFound();
            }

            return partyPackage;
        }

        // PUT: api/PartyPackages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartyPackage(int id, PartyPackage partyPackage)
        {
            if (id != partyPackage.PartyPackageId)
            {
                return BadRequest();
            }

            _context.Entry(partyPackage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartyPackageExists(id))
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

        // POST: api/PartyPackages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PartyPackage>> PostPartyPackage(PartyPackage partyPackage)
        {
            _context.PartyPackages.Add(partyPackage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartyPackage", new { id = partyPackage.PartyPackageId }, partyPackage);
        }

        // DELETE: api/PartyPackages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartyPackage(int id)
        {
            var partyPackage = await _context.PartyPackages.FindAsync(id);
            if (partyPackage == null)
            {
                return NotFound();
            }

            _context.PartyPackages.Remove(partyPackage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartyPackageExists(int id)
        {
            return _context.PartyPackages.Any(e => e.PartyPackageId == id);
        }
    }
}
