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
    public class CustomerInfoesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public CustomerInfoesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/CustomerInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInfo>>> GetCustomerInfos()
        {
            return await _context.CustomerInfos.ToListAsync();
        }

        // GET: api/CustomerInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInfo>> GetCustomerInfo(int id)
        {
            var customerInfo = await _context.CustomerInfos.FindAsync(id);

            if (customerInfo == null)
            {
                return NotFound();
            }

            return customerInfo;
        }

        // PUT: api/CustomerInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerInfo(int id, CustomerInfo customerInfo)
        {
            if (id != customerInfo.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customerInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInfoExists(id))
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

        // POST: api/CustomerInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("SaveCustomer")]
        public async Task<ActionResult<CustomerInfo>> PostCustomerInfo(CustomerInfo customerInfo)
        {
            if (customerInfo.CustomerId == 0)
            {
                // Add new customer
                _context.CustomerInfos.Add(customerInfo);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCustomerInfo", new { id = customerInfo.CustomerId }, customerInfo);
            }
            else
            {
                // Update existing customer
                var existingCustomer = await _context.CustomerInfos.FindAsync(customerInfo.CustomerId);
                if (existingCustomer == null)
                {
                    return NotFound($"Customer with ID {customerInfo.CustomerId} not found.");
                }

                // Update the existing customer properties
                existingCustomer.FirstName = customerInfo.FirstName;
                existingCustomer.LastName = customerInfo.LastName;
                existingCustomer.EmailAddress = customerInfo.EmailAddress;
                existingCustomer.PhoneNo = customerInfo.PhoneNo;
                existingCustomer.RelationshipId = customerInfo.RelationshipId;
                existingCustomer.OtherRelationshipId = customerInfo.OtherRelationshipId;
                existingCustomer.AlternatePhone = customerInfo.AlternatePhone;
                existingCustomer.Address = customerInfo.Address;
                existingCustomer.AddressTypeId = customerInfo.AddressTypeId;
                existingCustomer.City = customerInfo.City;
                existingCustomer.Zip = customerInfo.Zip;
                existingCustomer.StateId = customerInfo.StateId;
                existingCustomer.ChildrenId = customerInfo.ChildrenId;
                existingCustomer.ChildrenUnderAgeId = customerInfo.ChildrenUnderAgeId;
                existingCustomer.HonoreeName = customerInfo.HonoreeName;
                existingCustomer.HonoreeAge = customerInfo.HonoreeAge;
                existingCustomer.HeardResourceId = customerInfo.HeardResourceId;
                //existingCustomer.SpecifyOothther = customerInfo.SpecifyOther;
                existingCustomer.Comments = customerInfo.Comments;
                // Update other fields as needed

                _context.Entry(existingCustomer).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(existingCustomer);
            }
        }


        // DELETE: api/CustomerInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerInfo(int id)
        {
            var customerInfo = await _context.CustomerInfos.FindAsync(id);
            if (customerInfo == null)
            {
                return NotFound();
            }

            _context.CustomerInfos.Remove(customerInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerInfoExists(int id)
        {
            return _context.CustomerInfos.Any(e => e.CustomerId == id);
        }
    }
}
