using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.Models;
using ClownsCRMAPI.CustomModels;

namespace ClownsCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BouncesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public BouncesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/Bounces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bounce>>> GetBounces()
        {
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

            var BouncesQuery = _context.Bounces.AsQueryable();
            BouncesQuery = BouncesQuery.Where(o => o.BranchId == null || o.BranchId == BranchId);
            BouncesQuery = BouncesQuery.Where(o => o.CompanyId == null || o.CompanyId == CompanyId);

            return await BouncesQuery.ToListAsync();
        }

        // GET: api/Bounces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bounce>> GetBounce(int id)
        {
            var bounce = await _context.Bounces.FindAsync(id);

            if (bounce == null)
            {
                return NotFound();
            }

            return bounce;
        }

        // PUT: api/Bounces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBounce(int id, Bounce bounce)
        {
            if (id != bounce.BounceId)
            {
                return BadRequest();
            }

            _context.Entry(bounce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BounceExists(id))
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

        // POST: api/Bounces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bounce>> PostBounce(Bounce bounce)
        {
            _context.Bounces.Add(bounce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBounce", new { id = bounce.BounceId }, bounce);
        }

        // DELETE: api/Bounces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBounce(int id)
        {
            var bounce = await _context.Bounces.FindAsync(id);
            if (bounce == null)
            {
                return NotFound();
            }

            _context.Bounces.Remove(bounce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BounceExists(int id)
        {
            return _context.Bounces.Any(e => e.BounceId == id);
        }
    }
}
