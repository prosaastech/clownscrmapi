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
using static ClownsCRMAPI.CustomModels.Extensions;

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
        public async Task<ActionResult<int>> PostContractBookingPaymentInfo(ContractBookingPaymentInfoModel contractBookingPaymentInfoModel)
        {
            // Get BranchId and CompanyId from the token
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

            // Begin a new transaction
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ContractBookingPaymentInfo contractBookingPaymentInfo;

                    if (contractBookingPaymentInfoModel.BookingPaymentInfoId > 0)
                    {
                        // Check if the record exists and matches the BranchId and CompanyId
                        contractBookingPaymentInfo = await _context.ContractBookingPaymentInfos
                            .Where(p => p.BookingPaymentInfoId == contractBookingPaymentInfoModel.BookingPaymentInfoId &&
                                        p.BranchId == BranchId &&
                                        p.CompanyId == CompanyId)
                            .FirstOrDefaultAsync();

                        if (contractBookingPaymentInfo == null)
                        {
                            return NotFound(); // Return 404 if the record is not found or does not belong to the branch/company
                        }

                        // Update the fields for the existing record
                        contractBookingPaymentInfo = MapToEntity(contractBookingPaymentInfoModel, contractBookingPaymentInfo);
                    }
                    else
                    {
                        // Create a new ContractBookingPaymentInfo entity
                        contractBookingPaymentInfo = new ContractBookingPaymentInfo
                        {
                            BranchId = BranchId,
                            CompanyId = CompanyId
                        };

                        // Map data from model to the new entity
                        contractBookingPaymentInfo = MapToEntity(contractBookingPaymentInfoModel, contractBookingPaymentInfo);

                        // Add a new record to the database
                        _context.ContractBookingPaymentInfos.Add(contractBookingPaymentInfo);
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    if (contractBookingPaymentInfoModel.ContractStatusId == 0 ||
                        contractBookingPaymentInfoModel.ContractStatusId == null)
                    {
                        contractBookingPaymentInfoModel.ContractStatusId = (int)enumContractStatus.Quoted;
                        var PkgInfo = _context.ContractPackageInfos.Where(o => o.ContractId == contractBookingPaymentInfo.ContractId && o.CustomerId == contractBookingPaymentInfo.CustomerId).FirstOrDefault();
                        if (PkgInfo != null)
                        {
                            if (PkgInfo.Deposit !=null && PkgInfo.Deposit != 0)
                            {
                                contractBookingPaymentInfoModel.ContractStatusId = (int)enumContractStatus.Booked;
                            }
                        }
                    }


                    var contract = _context.ContractTimeTeamInfos.Where(o => o.ContractId == contractBookingPaymentInfo.ContractId && o.CustomerId == contractBookingPaymentInfo.CustomerId).ToList();
                    foreach (var item in contract)
                    {
                        item.ContractStatusId = contractBookingPaymentInfoModel.ContractStatusId;
                    }
                    await _context.SaveChangesAsync(); 

                    // Commit the transaction
                    await transaction.CommitAsync();

                    // Return the saved or updated BookingPaymentInfoId
                    return Ok(contractBookingPaymentInfo.BookingPaymentInfoId);
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of any error
                    await transaction.RollbackAsync();

                    // Log the error (optional)
                    Console.WriteLine($"Error: {ex.Message}");

                    // Return an error response
                    return StatusCode(500, "An error occurred while saving the data.");
                }
            }
        }

        // Helper method to map model to entity
        private ContractBookingPaymentInfo MapToEntity(ContractBookingPaymentInfoModel model, ContractBookingPaymentInfo entity)
        {
            // Map the fields from the model to the entity
            entity.BookingPaymentInfoId = model.BookingPaymentInfoId;
            entity.CustomerId = model.CustomerId;
            entity.ContractId = model.ContractId;
            entity.BranchId = model.BranchId ?? entity.BranchId; // Keeping the branch ID set in case of new creation
            entity.CompanyId = model.CompanyId ?? entity.CompanyId; // Same for company ID
            entity.CardTypeId = model.CardTypeId;
            entity.ExpireMonthYear = model.ExpireMonthYear;
            entity.Cvv = model.Cvv;
            entity.CardTypeId2 = model.CardTypeId2;
            entity.ExpireMonthYear2 = model.ExpireMonthYear2;
            entity.Cvv2 = model.Cvv2;
            entity.PaymentStatusId = model.PaymentStatusId;
            entity.BillingAddress = model.BillingAddress;
            entity.UseAddress = model.UseAddress;
            entity.CardNumber = model.CardNumber;
            entity.CardNumber2 = model.CardNumber2;
            //entity.ContractStatusId = model.ContractStatusId;

            return entity;
        }

        //public async Task<ActionResult<int>> PostContractBookingPaymentInfo(ContractBookingPaymentInfoModel contractBookingPaymentInfo)
        //{
        //    // Get BranchId and CompanyId from the token
        //    int BranchId = TokenHelper.GetBranchId(HttpContext);
        //    int CompanyId = TokenHelper.GetCompanyId(HttpContext);

        //    if (contractBookingPaymentInfo.BookingPaymentInfoId > 0)
        //    {
        //        // Check if the record exists and matches the BranchId and CompanyId
        //        var existingPaymentInfo = await _context.ContractBookingPaymentInfos
        //            .Where(p => p.BookingPaymentInfoId == contractBookingPaymentInfo.BookingPaymentInfoId &&
        //                        p.BranchId == BranchId &&
        //                        p.CompanyId == CompanyId)
        //            .FirstOrDefaultAsync();

        //        if (existingPaymentInfo == null)
        //        {
        //            return NotFound(); // Return 404 if the record is not found or does not belong to the branch/company
        //        }
        //        contractBookingPaymentInfo.BranchId = BranchId;
        //        contractBookingPaymentInfo.CompanyId = CompanyId;



        //        // Update the fields for the existing record
        //        _context.Entry(existingPaymentInfo).CurrentValues.SetValues(contractBookingPaymentInfo);
        //    }
        //    else
        //    {
        //        // Add a new record and set BranchId and CompanyId
        //        contractBookingPaymentInfo.BranchId = BranchId;
        //        contractBookingPaymentInfo.CompanyId = CompanyId;
        //        _context.ContractBookingPaymentInfos.Add(contractBookingPaymentInfo);
        //    }

        //    // Save changes to the database
        //    await _context.SaveChangesAsync();

        //    // Return the saved or updated BookingPaymentInfoId
        //    return Ok(contractBookingPaymentInfo.BookingPaymentInfoId);
        //}



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
