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
    public class ContractPackageInfoCharactersController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractPackageInfoCharactersController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ContractPackageInfoCharacters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractPackageInfoCharacter>>> GetContractPackageInfoCharacters()
        {
            return await _context.ContractPackageInfoCharacters.ToListAsync();
        }

        // GET: api/ContractPackageInfoCharacters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractPackageInfoCharacter>> GetContractPackageInfoCharacter(int id)
        {
            var contractPackageInfoCharacter = await _context.ContractPackageInfoCharacters.FindAsync(id);

            if (contractPackageInfoCharacter == null)
            {
                return NotFound();
            }

            return contractPackageInfoCharacter;
        }

        // PUT: api/ContractPackageInfoCharacters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractPackageInfoCharacter(int id, ContractPackageInfoCharacter contractPackageInfoCharacter)
        {
            if (id != contractPackageInfoCharacter.ContractPackageInfoCharacterId)
            {
                return BadRequest();
            }

            _context.Entry(contractPackageInfoCharacter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractPackageInfoCharacterExists(id))
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

        // POST: api/ContractPackageInfoCharacters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContractPackageInfoCharacter>> PostContractPackageInfoCharacter(ContractPackageInfoCharacter contractPackageInfoCharacter)
        {
            _context.ContractPackageInfoCharacters.Add(contractPackageInfoCharacter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractPackageInfoCharacter", new { id = contractPackageInfoCharacter.ContractPackageInfoCharacterId }, contractPackageInfoCharacter);
        }

        // DELETE: api/ContractPackageInfoCharacters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractPackageInfoCharacter(int id)
        {
            var contractPackageInfoCharacter = await _context.ContractPackageInfoCharacters.FindAsync(id);
            if (contractPackageInfoCharacter == null)
            {
                return NotFound();
            }

            _context.ContractPackageInfoCharacters.Remove(contractPackageInfoCharacter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractPackageInfoCharacterExists(int id)
        {
            return _context.ContractPackageInfoCharacters.Any(e => e.ContractPackageInfoCharacterId == id);
        }
    }
}
