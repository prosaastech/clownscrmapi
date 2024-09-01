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
    public class CardOptionsController : ControllerBase
    {
        private readonly ClownsContext _context;

        public CardOptionsController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/CardOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardOption>>> GetCardOptions()
        {
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

            var CardOptionsQuery = _context.CardOptions.AsQueryable();
            CardOptionsQuery = CardOptionsQuery.Where(o => o.BranchId == null || o.BranchId == BranchId);
            CardOptionsQuery = CardOptionsQuery.Where(o => o.CompanyId == null || o.CompanyId == CompanyId);

            return await CardOptionsQuery.ToListAsync();
        }

        // GET: api/CardOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardOption>> GetCardOption(int id)
        {
            var cardOption = await _context.CardOptions.FindAsync(id);

            if (cardOption == null)
            {
                return NotFound();
            }

            return cardOption;
        }

        // PUT: api/CardOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardOption(int id, CardOption cardOption)
        {
            if (id != cardOption.CardOptionId)
            {
                return BadRequest();
            }

            _context.Entry(cardOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardOptionExists(id))
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

        // POST: api/CardOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardOption>> PostCardOption(CardOption cardOption)
        {
            _context.CardOptions.Add(cardOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardOption", new { id = cardOption.CardOptionId }, cardOption);
        }

        // DELETE: api/CardOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardOption(int id)
        {
            var cardOption = await _context.CardOptions.FindAsync(id);
            if (cardOption == null)
            {
                return NotFound();
            }

            _context.CardOptions.Remove(cardOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardOptionExists(int id)
        {
            return _context.CardOptions.Any(e => e.CardOptionId == id);
        }
    }
}
