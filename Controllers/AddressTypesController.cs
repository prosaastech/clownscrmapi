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
    public class AddressTypesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public AddressTypesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/AddressTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> GetAddressTypes()
        {
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

            var AddressTypesQuery = _context.AddressTypes.AsQueryable();
            AddressTypesQuery = AddressTypesQuery.Where(o => o.BranchId == null || o.BranchId == BranchId);
            AddressTypesQuery = AddressTypesQuery.Where(o => o.CompanyId == null || o.CompanyId == CompanyId);

            return await AddressTypesQuery.ToListAsync();
        }

        // GET: api/AddressTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressType>> GetAddressType(int id)
        {
            var addressType = await _context.AddressTypes.FindAsync(id);

            if (addressType == null)
            {
                return NotFound();
            }

            return addressType;
        }

        // PUT: api/AddressTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressType(int id, AddressType addressType)
        {
            if (id != addressType.AddressTypeId)
            {
                return BadRequest();
            }

            _context.Entry(addressType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypeExists(id))
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

        // POST: api/AddressTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressType>> PostAddressType(AddressType addressType)
        {
            _context.AddressTypes.Add(addressType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressType", new { id = addressType.AddressTypeId }, addressType);
        }

        // DELETE: api/AddressTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressType(int id)
        {
            var addressType = await _context.AddressTypes.FindAsync(id);
            if (addressType == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(addressType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressTypeExists(int id)
        {
            return _context.AddressTypes.Any(e => e.AddressTypeId == id);
        }
    }
}
