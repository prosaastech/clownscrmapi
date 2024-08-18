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
    public class ChildrenUnderAgesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ChildrenUnderAgesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ChildrenUnderAges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChildrenUnderAge>>> GetChildrenUnderAges()
        {
            return await _context.ChildrenUnderAges.ToListAsync();
        }

        // GET: api/ChildrenUnderAges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChildrenUnderAge>> GetChildrenUnderAge(int id)
        {
            var childrenUnderAge = await _context.ChildrenUnderAges.FindAsync(id);

            if (childrenUnderAge == null)
            {
                return NotFound();
            }

            return childrenUnderAge;
        }

        // PUT: api/ChildrenUnderAges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChildrenUnderAge(int id, ChildrenUnderAge childrenUnderAge)
        {
            if (id != childrenUnderAge.ChildrenUnderAgeId)
            {
                return BadRequest();
            }

            _context.Entry(childrenUnderAge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildrenUnderAgeExists(id))
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

        // POST: api/ChildrenUnderAges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChildrenUnderAge>> PostChildrenUnderAge(ChildrenUnderAge childrenUnderAge)
        {
            _context.ChildrenUnderAges.Add(childrenUnderAge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChildrenUnderAge", new { id = childrenUnderAge.ChildrenUnderAgeId }, childrenUnderAge);
        }

        // DELETE: api/ChildrenUnderAges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChildrenUnderAge(int id)
        {
            var childrenUnderAge = await _context.ChildrenUnderAges.FindAsync(id);
            if (childrenUnderAge == null)
            {
                return NotFound();
            }

            _context.ChildrenUnderAges.Remove(childrenUnderAge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChildrenUnderAgeExists(int id)
        {
            return _context.ChildrenUnderAges.Any(e => e.ChildrenUnderAgeId == id);
        }
    }
}
