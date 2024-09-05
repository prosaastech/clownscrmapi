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
    public class ContractBookingPaymentInfoesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractBookingPaymentInfoesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ContractBookingPaymentInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractBookingPaymentInfo>>> GetContractBookingPaymentInfos()
        {
            return await _context.ContractBookingPaymentInfos.ToListAsync();
        }

        // GET: api/ContractBookingPaymentInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractBookingPaymentInfo>> GetContractBookingPaymentInfo(int id)
        {
            var contractBookingPaymentInfo = await _context.ContractBookingPaymentInfos.FindAsync(id);

            if (contractBookingPaymentInfo == null)
            {
                return NotFound();
            }

            return contractBookingPaymentInfo;
        }

        // PUT: api/ContractBookingPaymentInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractBookingPaymentInfo(int id, ContractBookingPaymentInfo contractBookingPaymentInfo)
        {
            if (id != contractBookingPaymentInfo.BookingPaymentInfoId)
            {
                return BadRequest();
            }

            _context.Entry(contractBookingPaymentInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractBookingPaymentInfoExists(id))
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

        // POST: api/ContractBookingPaymentInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("SaveBookingPaymentInfo")]
        public async Task<ActionResult<int>> PostContractBookingPaymentInfo(ContractBookingPaymentInfo contractBookingPaymentInfo)
        {
            if (contractBookingPaymentInfo.BookingPaymentInfoId > 0)
            {
                 var existingPaymentInfo = await _context.ContractBookingPaymentInfos
                    .FindAsync(contractBookingPaymentInfo.BookingPaymentInfoId);

                if (existingPaymentInfo == null)
                {
                    return NotFound(); // Return 404 if the record is not found
                }

                // Update the fields
                _context.Entry(existingPaymentInfo).CurrentValues.SetValues(contractBookingPaymentInfo);
            }
            else
            {
                // Add new record
                _context.ContractBookingPaymentInfos.Add(contractBookingPaymentInfo);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the saved BookingPaymentInfoId
            return Ok(contractBookingPaymentInfo.BookingPaymentInfoId);
        }


        // DELETE: api/ContractBookingPaymentInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractBookingPaymentInfo(int id)
        {
            var contractBookingPaymentInfo = await _context.ContractBookingPaymentInfos.FindAsync(id);
            if (contractBookingPaymentInfo == null)
            {
                return NotFound();
            }

            _context.ContractBookingPaymentInfos.Remove(contractBookingPaymentInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractBookingPaymentInfoExists(int id)
        {
            return _context.ContractBookingPaymentInfos.Any(e => e.BookingPaymentInfoId == id);
        }
    }
}
