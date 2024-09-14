using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.Models;
using ClownsCRMAPI.CustomModels;
using NuGet.Packaging;
using Microsoft.AspNetCore.Authorization;

namespace ClownsCRMAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContractPackageInfoesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractPackageInfoesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ContractPackageInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractPackageInfo>>> GetContractPackageInfos()
        {
            return await _context.ContractPackageInfos.ToListAsync();
        }

        // GET: api/ContractPackageInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractPackageInfo>> GetContractPackageInfo(int id)
        {
            var contractPackageInfo = await _context.ContractPackageInfos.FindAsync(id);

            if (contractPackageInfo == null)
            {
                return NotFound();
            }

            return contractPackageInfo;
        }

        // PUT: api/ContractPackageInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractPackageInfo(int id, ContractPackageInfo contractPackageInfo)
        {
            if (id != contractPackageInfo.PackageInfoId)
            {
                return BadRequest();
            }

            _context.Entry(contractPackageInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractPackageInfoExists(id))
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

        // POST: api/ContractPackageInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("SavePackageInfo")]
        //public async Task<ActionResult<ContractPackageInfo>> SavePackageInfo(ContractPackageInfoModel contractPackageInfo)
        //{
        //    //_context.ContractPackageInfos.Add(contractPackageInfo);
        //    //await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetContractPackageInfo", new { id = contractPackageInfo.PackageInfoId }, contractPackageInfo);
        //}
        [HttpPost("SavePackageInfo")]
        public async Task<ActionResult<int>> SavePackageInfo(ContractPackageInfoModel contractPackageInfoModel)
        {
            int packageInfoId;
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

            // Check if the record exists by checking PackageInfoId
            if (contractPackageInfoModel.PackageInfoId > 0)
            {
                // Update existing record
                var existingPackageInfo = await _context.ContractPackageInfos
                    .Include(c => c.ContractPackageInfoCharacters)
                    .Include(a => a.ContractPackageInfoAddons)
                    .Include(b => b.ContractPackageInfoBounces)
                    .Where(x => x.PackageInfoId == contractPackageInfoModel.PackageInfoId
                                && x.BranchId == BranchId
                                && x.CompanyId == CompanyId)
                    .FirstOrDefaultAsync();

                if (existingPackageInfo == null)
                {
                    return NotFound(); // If no matching record is found
                }

                // Update fields
                existingPackageInfo.CategoryId = contractPackageInfoModel.CategoryId;
                existingPackageInfo.PartyPackageId = contractPackageInfoModel.PartyPackageId;
                existingPackageInfo.Price = contractPackageInfoModel.Price;
                existingPackageInfo.Tax = contractPackageInfoModel.Tax;
                existingPackageInfo.Tip = contractPackageInfoModel.Tip;
                existingPackageInfo.Description = contractPackageInfoModel.Description;
                existingPackageInfo.ParkingFees = contractPackageInfoModel.ParkingFees;
                existingPackageInfo.TollFees = contractPackageInfoModel.TollFees;
                existingPackageInfo.Deposit = contractPackageInfoModel.Deposit;
                existingPackageInfo.Tip2 = contractPackageInfoModel.Tip2;
                existingPackageInfo.Subtract = contractPackageInfoModel.Substract;
                existingPackageInfo.TotalBalance = contractPackageInfoModel.TotalBalance;
                existingPackageInfo.ContractId = contractPackageInfoModel.ContractId;
                existingPackageInfo.CustomerId = contractPackageInfoModel.CustomerId;

                // Update related entities
                // Clear existing and add new
                _context.ContractPackageInfoCharacters.RemoveRange(existingPackageInfo.ContractPackageInfoCharacters);
                _context.ContractPackageInfoCharacters.AddRange(
                    contractPackageInfoModel.Characters.Select(c => new ContractPackageInfoCharacter
                    {
                        CharacterId = c.CharacterId,
                        Price = c.Price,
                        PackageInfoId = existingPackageInfo.PackageInfoId,
                        BranchId = existingPackageInfo.BranchId,
                        CompanyId = existingPackageInfo.CompanyId
                    })
                );

                _context.ContractPackageInfoAddons.RemoveRange(existingPackageInfo.ContractPackageInfoAddons);
                _context.ContractPackageInfoAddons.AddRange(
                    contractPackageInfoModel.Addons.Select(a => new ContractPackageInfoAddon
                    {
                        AddonId = a.AddonId,
                        Price = a.Price,
                        PackageInfoId = existingPackageInfo.PackageInfoId,
                        BranchId = existingPackageInfo.BranchId,
                        CompanyId = existingPackageInfo.CompanyId
                    })
                );

                _context.ContractPackageInfoBounces.RemoveRange(existingPackageInfo.ContractPackageInfoBounces);
                _context.ContractPackageInfoBounces.AddRange(
                    contractPackageInfoModel.Bounces.Select(b => new ContractPackageInfoBounce
                    {
                        BounceId = b.BounceId,
                        Price = b.Price,
                        PackageInfoId = existingPackageInfo.PackageInfoId,
                        BranchId = existingPackageInfo.BranchId,
                        CompanyId = existingPackageInfo.CompanyId
                    })
                );
                await _context.SaveChangesAsync();

                packageInfoId = existingPackageInfo.PackageInfoId;
            }
            else
            {
                // Insert new record
                var newPackageInfo = new ContractPackageInfo
                {
                    CategoryId = contractPackageInfoModel.CategoryId,
                    PartyPackageId = contractPackageInfoModel.PartyPackageId,
                    Price = contractPackageInfoModel.Price,
                    Tax = contractPackageInfoModel.Tax,
                    Tip = contractPackageInfoModel.Tip,
                    Description = contractPackageInfoModel.Description,
                    ParkingFees = contractPackageInfoModel.ParkingFees,
                    TollFees = contractPackageInfoModel.TollFees,
                    Deposit = contractPackageInfoModel.Deposit,
                    Tip2 = contractPackageInfoModel.Tip2,
                    Subtract = contractPackageInfoModel.Substract,
                    TotalBalance = contractPackageInfoModel.TotalBalance,
                    BranchId = BranchId,
                    CompanyId = CompanyId,
                    ContractId = contractPackageInfoModel.ContractId,
                    CustomerId = contractPackageInfoModel.CustomerId
                };

                // Add related entities
                newPackageInfo.ContractPackageInfoCharacters.AddRange(
                    contractPackageInfoModel.Characters.Select(c => new ContractPackageInfoCharacter
                    {
                        CharacterId = c.CharacterId,
                        Price = c.Price,
                        BranchId = contractPackageInfoModel.BranchId,
                        CompanyId = contractPackageInfoModel.CompanyId
                    })
                );

                newPackageInfo.ContractPackageInfoAddons.AddRange(
                    contractPackageInfoModel.Addons.Select(a => new ContractPackageInfoAddon
                    {
                        AddonId = a.AddonId,
                        Price = a.Price,
                        BranchId = contractPackageInfoModel.BranchId,
                        CompanyId = contractPackageInfoModel.CompanyId
                    })
                );

                newPackageInfo.ContractPackageInfoBounces.AddRange(
                    contractPackageInfoModel.Bounces.Select(b => new ContractPackageInfoBounce
                    {
                        BounceId = b.BounceId,
                        Price = b.Price,
                        BranchId = contractPackageInfoModel.BranchId,
                        CompanyId = contractPackageInfoModel.CompanyId
                    })
                );

                _context.ContractPackageInfos.Add(newPackageInfo);
                await _context.SaveChangesAsync();

                packageInfoId = newPackageInfo.PackageInfoId;
            }

            // Return the PackageInfoId for storing in the session
            return Ok(packageInfoId);
        }


        // DELETE: api/ContractPackageInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractPackageInfo(int id)
        {
            var contractPackageInfo = await _context.ContractPackageInfos.FindAsync(id);
            if (contractPackageInfo == null)
            {
                return NotFound();
            }

            _context.ContractPackageInfos.Remove(contractPackageInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractPackageInfoExists(int id)
        {
            return _context.ContractPackageInfos.Any(e => e.PackageInfoId == id);
        }
    }
}
