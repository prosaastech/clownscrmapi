﻿using System;
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
    public class RelationshipsController : ControllerBase
    {
        private readonly ClownsContext _context;

        public RelationshipsController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/Relationships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relationship>>> GetRelationships()
        {
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

            var relationshipsQuery = _context.Relationships.AsQueryable();
            relationshipsQuery = relationshipsQuery.Where(o => o.BranchId == null || o.BranchId == BranchId);
            relationshipsQuery = relationshipsQuery.Where(o => o.CompanyId == null || o.CompanyId == CompanyId);

            return await relationshipsQuery.ToListAsync();
        }

        // GET: api/Relationships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Relationship>> GetRelationship(int id)
        {
            var relationship = await _context.Relationships.FindAsync(id);

            if (relationship == null)
            {
                return NotFound();
            }

            return relationship;
        }

        // PUT: api/Relationships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelationship(int id, Relationship relationship)
        {
            if (id != relationship.RelationshipId)
            {
                return BadRequest();
            }

            _context.Entry(relationship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipExists(id))
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

        // POST: api/Relationships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Relationship>> PostRelationship(Relationship relationship)
        {
            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelationship", new { id = relationship.RelationshipId }, relationship);
        }

        // DELETE: api/Relationships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelationship(int id)
        {
            var relationship = await _context.Relationships.FindAsync(id);
            if (relationship == null)
            {
                return NotFound();
            }

            _context.Relationships.Remove(relationship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RelationshipExists(int id)
        {
            return _context.Relationships.Any(e => e.RelationshipId == id);
        }
    }
}
