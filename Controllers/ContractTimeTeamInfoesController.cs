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
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace ClownsCRMAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTimeTeamInfoesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractTimeTeamInfoesController(ClownsContext context)
        {
            _context = context;
        }

        [HttpGet("getContractData")]
        public async Task<IActionResult> getContractData(int CustomerId,int ContractId)
        {
           
            try
            {
                 int branchId = TokenHelper.GetBranchId(HttpContext);
                int companyId = TokenHelper.GetCompanyId(HttpContext);

                var query = (from customer in _context.CustomerInfos

                                 // Left join with ContractEventInfos
                             join eventInfo in _context.ContractEventInfos
                             on customer.CustomerId equals (int?)eventInfo.CustomerId into customerEvents
                             from eventInfo in customerEvents.DefaultIfEmpty()

                                 // Left join with ContractTimeTeamInfos (using composite keys)
                             join timeTeamInfo in _context.ContractTimeTeamInfos
                             on new { CustomerId = (int?)customer.CustomerId, ContractId = (int?)eventInfo.ContractId, BranchId = (int?)customer.BranchId, CompanyId = (int?)customer.CompanyId }
                             equals new { CustomerId = (int?)timeTeamInfo.CustomerId, ContractId = (int?)timeTeamInfo.ContractId, BranchId = (int?)timeTeamInfo.BranchId, CompanyId = (int?)timeTeamInfo.CompanyId } into eventTimes
                             from timeTeamInfo in eventTimes.DefaultIfEmpty()

                                 // Left join with States
                             join state in _context.States
                             on customer.StateId equals (int?)state.StateId into stateInfo
                             from state in stateInfo.DefaultIfEmpty()

                                 // Left join with Contract_PackageInfo based on CustomerId and ContractId
                             join contractPackage in _context.ContractPackageInfos
                             on new { CustomerId = (int?)customer.CustomerId, ContractId = (int?)timeTeamInfo.ContractId }
                             equals new { CustomerId = (int?)contractPackage.CustomerId, ContractId = (int?)contractPackage.ContractId } into contractPackages
                             from contractPackage in contractPackages.DefaultIfEmpty()

                                 // Left join with PartyPackage based on PartyPackageId from Contract_PackageInfo
                             join partyPackage in _context.PartyPackages
                             on (int?)contractPackage.PartyPackageId equals (int?)partyPackage.PartyPackageId into partyPackageInfo
                             from partyPackage in partyPackageInfo.DefaultIfEmpty()

                                 // Left join with Contract_PackageInfo_Character to get related characters
                             join contractPackageCharacter in _context.ContractPackageInfoCharacters
                             on contractPackage.PackageInfoId equals contractPackageCharacter.PackageInfoId into packageCharacters
                             from contractPackageCharacter in packageCharacters.DefaultIfEmpty()

                                 // Left join with Character to get character names
                             join character in _context.Characters
                             on contractPackageCharacter.CharacterId equals character.CharacterId into characterInfo
                             from character in characterInfo.DefaultIfEmpty()

                                 // Left join with Contract_PackageInfo_Bounce to get related bounces
                             join contractPackageBounce in _context.ContractPackageInfoBounces
                             on contractPackage.PackageInfoId equals contractPackageBounce.PackageInfoId into packageBounces
                             from contractPackageBounce in packageBounces.DefaultIfEmpty()

                                 // Left join with Bounce to get bounce names
                             join bounce in _context.Bounces
                             on contractPackageBounce.BounceId equals bounce.BounceId into bounceInfo
                             from bounce in bounceInfo.DefaultIfEmpty()

                                 // Left join with Contract_PackageInfo_Addon to get related addons
                             join contractPackageAddon in _context.ContractPackageInfoAddons
                             on contractPackage.PackageInfoId equals contractPackageAddon.PackageInfoId into packageAddons
                             from contractPackageAddon in packageAddons.DefaultIfEmpty()

                                 // Left join with Addon to get addon names
                             join addon in _context.Addons
                             on contractPackageAddon.AddonId equals addon.AddonId into addonInfo
                             from addon in addonInfo.DefaultIfEmpty()

                             join contractBookingPayment in _context.ContractBookingPaymentInfos
                             on new { CustomerId = (int?)customer.CustomerId, ContractId = (int?)timeTeamInfo.ContractId }
                             equals new { CustomerId = (int?)contractBookingPayment.CustomerId, ContractId = (int?)contractBookingPayment.ContractId } into contractBookingPayments
                             from contractBookingPayment in contractBookingPayments.DefaultIfEmpty()

                                 // Where clause to filter by BranchId and CompanyId
                             where customer.BranchId == branchId && customer.CompanyId == companyId

                               select new
                             {
                                 customer.FirstName,
                                 customer.LastName,
                                 customer.EmailAddress,
                                 customer.City,
                                 customer.PhoneNo,
                                 customer.HonoreeName,
                                 state.StateId,
                                 EventDate = eventInfo.EventInfoEventDate.HasValue
                                            ? (DateTime?)eventInfo.EventInfoEventDate.Value.ToDateTime(new TimeOnly(0, 0))
                                            : (DateTime?)null,
                                 timeTeamInfo.ContractNo,
                                 timeTeamInfo.ContractId,
                                 ContractDate = timeTeamInfo.EntryDate,
                                 contractPackage.PartyPackageId,
                                 partyPackage.PartyPackageName,
                                 contractPackage.CategoryId,
                                 eventInfo.EventInfoVenue,
                                 contractBookingPayment.PaymentStatusId,
                                 timeTeamInfo.ContractStatusId,
                                 customer.CustomerId,
                                 eventInfo.EventInfoPartyStartTime,
                                 eventInfo.EventInfoPartyEndTime,
                                 customer.AddressTypeId

                             }).AsQueryable();



                if (CustomerId !=0 && CustomerId !=0)
                    query = query.Where(x => x.CustomerId.Equals(CustomerId));

                if (ContractId != 0 && ContractId != 0)
                    query = query.Where(x => x.ContractId.Equals(ContractId));


                var totalCount = await query.CountAsync();

                // Apply pagination
                var results = await query 
                    .Select(x => new SearchResultDto
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        EmailAddress = x.EmailAddress,
                        ContractId = x.ContractId,
                        ContractNumber = x.ContractNo,
                        ContractDate = x.ContractDate,
                        EventDate = x.EventDate,
                        StateId = x.StateId,
                        //StateName = x.StateName,
                        City = x.City,
                        //PackageName = x.PackageName,
                        primaryHonoree = x.HonoreeName,
                        //characters = x.Characters,
                        //bounces = x.Bounces,
                        //addOns = x.Addons,
                        //approval = x.Approval,
                        //confirmation = x.Confirmation,
                        ContractStatusId = x.ContractStatusId,
                        CustomerId = x.CustomerId,
                        AddressTypeId = x.AddressTypeId

                    })
                    .FirstOrDefaultAsync();

                // Return paginated results
                return Ok(results);
            }
            catch (Exception ex)
            {
                // Log exception (if you have a logging mechanism)
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("getAllContractsDateWise")]
        public async Task<ActionResult<object>> GetContractTimeTeamInfos(DateTime? date)
        {
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

             
            // Start with the base query
            IQueryable<ContractTimeTeamInfo> query = _context.ContractTimeTeamInfos
                .Include(ctti => ctti.TimeSlot)
                .Include(ctti => ctti.Team);

            // Apply filtering if the date parameter is provided
            if (date.HasValue)
            {
                var dateToCompare = date.Value.Date; // Extract the date part without the time component
                query = query.Where(ctti => ctti.Date.ToDateTime(TimeOnly.MinValue).Date == dateToCompare);
            }

            // Execute the query and get the results
            var listOfContractTimeInfo = await query
                                                .Where(o=> (o.BranchId == null ||o.BranchId == BranchId) &&
                                                o.CompanyId == null || o.CompanyId == CompanyId)
                                                .ToListAsync();

            // Extract teams and time slots
            var teams = listOfContractTimeInfo
                .Where(o => (o.BranchId == null || o.BranchId == BranchId) &&
                                                o.CompanyId == null || o.CompanyId == CompanyId)
                .Select(ctti => new { ctti.Team.TeamId, ctti.Team.TeamNo })
                .Distinct()
                .ToList();

            var timeSlots = listOfContractTimeInfo
                 .Where(o => (o.BranchId == null || o.BranchId == BranchId) &&
                                                o.CompanyId == null || o.CompanyId == CompanyId)
                 .Select(ctti => new { ctti.TimeSlot.TimeSlotId, ctti.TimeSlot.Time })
                 .Distinct()
                 .ToList();

            // Prepare the response
            var response = new
            {
                contracts = listOfContractTimeInfo.Select(ctti => new
                {
                    ctti.ContractTimeTeamInfoId,
                    Date = ctti.Date.ToString("yyyy-MM-dd"),
                    TeamId = ctti.Team.TeamId,
                    TeamNo = ctti.Team.TeamNo,
                    TimeSlotId = ctti.TimeSlot.TimeSlotId,
                    Time = ctti.TimeSlot.Time
                }).ToList(),
                teams,
                timeSlots
            };

            return Ok(response);
        }


        // GET: api/ContractTimeTeamInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractTimeTeamInfo>>> GetContractTimeTeamInfos()
        {
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

            var listOfContractTimeInfo = await _context.ContractTimeTeamInfos
                         .Where(o => (o.BranchId == null || o.BranchId == BranchId) &&
                                     (o.CompanyId == null || o.CompanyId == CompanyId))
                         .Select(ctti => new
                         {
                             ContractTimeInfo = ctti,
                             TimeSlot = ctti.TimeSlot != null &&
                                        (ctti.TimeSlot.BranchId == null || ctti.TimeSlot.BranchId == BranchId) &&
                                        (ctti.TimeSlot.CompanyId == null || ctti.TimeSlot.CompanyId == CompanyId)
                                        ? ctti.TimeSlot : null,
                             Team = ctti.Team != null &&
                                    (ctti.Team.BranchId == null || ctti.Team.BranchId == BranchId) &&
                                    (ctti.Team.CompanyId == null || ctti.Team.CompanyId == CompanyId)
                                    ? ctti.Team : null
                         })
                         .Where(result => result.TimeSlot != null && result.Team != null)
                         .ToListAsync();

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true // Optional: for pretty printing
            };

            var json = JsonSerializer.Serialize(listOfContractTimeInfo, jsonOptions);

            return Content(json, "application/json");
        }

        // GET: api/ContractTimeTeamInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractTimeTeamInfo>> GetContractTimeTeamInfo(long id)
        {
            var contractTimeTeamInfo = await _context.ContractTimeTeamInfos.FindAsync(id);

            if (contractTimeTeamInfo == null)
            {
                return NotFound();
            }

            return contractTimeTeamInfo;
        }

        // PUT: api/ContractTimeTeamInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractTimeTeamInfo(long id, ContractTimeTeamInfo contractTimeTeamInfo)
        {
            if (id != contractTimeTeamInfo.ContractTimeTeamInfoId)
            {
                return BadRequest();
            }

            _context.Entry(contractTimeTeamInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractTimeTeamInfoExists(id))
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

        // POST: api/ContractTimeTeamInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContractTimeTeamInfo>> PostContractTimeTeamInfo(ContractTimeTeamInfoModel contractTimeTeamInfo)
        {
            ContractTimeTeamInfo contractTimeTeamInfo1 = new ContractTimeTeamInfo();
            contractTimeTeamInfo1.TeamId = contractTimeTeamInfo.TeamId;
            contractTimeTeamInfo1.TimeSlotId = contractTimeTeamInfo.TimeSlotId;
            contractTimeTeamInfo1.ContractId = contractTimeTeamInfo.ContractId;
            // Convert DateTime to DateOnly
            contractTimeTeamInfo1.Date = DateOnly.FromDateTime(contractTimeTeamInfo.Date);
            _context.ContractTimeTeamInfos.Add(contractTimeTeamInfo1);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractTimeTeamInfo", new { id = contractTimeTeamInfo1.ContractTimeTeamInfoId }, contractTimeTeamInfo1);
        }

        // DELETE: api/ContractTimeTeamInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractTimeTeamInfo(long id)
        {
            var contractTimeTeamInfo = await _context.ContractTimeTeamInfos.FindAsync(id);
            if (contractTimeTeamInfo == null)
            {
                return NotFound();
            }

            _context.ContractTimeTeamInfos.Remove(contractTimeTeamInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractTimeTeamInfoExists(long id)
        {
            return _context.ContractTimeTeamInfos.Any(e => e.ContractTimeTeamInfoId == id);
        }
    }
}
