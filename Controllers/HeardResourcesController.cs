using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.Models;
using Microsoft.AspNetCore.Authorization;
using ClownsCRMAPI.CustomModels;

namespace ClownsCRMAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HeardResourcesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public HeardResourcesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/HeardResources
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeardResource>>> GetHeardResources()
        {
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

            var HeardResourcesQuery = _context.HeardResources.AsQueryable();
            HeardResourcesQuery = HeardResourcesQuery.Where(o => o.BranchId == null || o.BranchId == BranchId);
            HeardResourcesQuery = HeardResourcesQuery.Where(o => o.CompanyId == null || o.CompanyId == CompanyId);

            return await HeardResourcesQuery.ToListAsync();
        }

        // GET: api/HeardResources/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HeardResource>> GetHeardResource(int id)
        {
            var heardResource = await _context.HeardResources.FindAsync(id);

            if (heardResource == null)
            {
                return NotFound();
            }

            return heardResource;
        }

        // PUT: api/HeardResources/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeardResource(int id, HeardResource heardResource)
        {
            if (id != heardResource.HeardResourceId)
            {
                return BadRequest();
            }

            _context.Entry(heardResource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeardResourceExists(id))
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

        // POST: api/HeardResources
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HeardResource>> PostHeardResource(HeardResource heardResource)
        {
            _context.HeardResources.Add(heardResource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHeardResource", new { id = heardResource.HeardResourceId }, heardResource);
        }

        // DELETE: api/HeardResources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeardResource(int id)
        {
            var heardResource = await _context.HeardResources.FindAsync(id);
            if (heardResource == null)
            {
                return NotFound();
            }

            _context.HeardResources.Remove(heardResource);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeardResourceExists(int id)
        {
            return _context.HeardResources.Any(e => e.HeardResourceId == id);
        }
    }
}
