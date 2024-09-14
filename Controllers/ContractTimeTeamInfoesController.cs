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
using NuGet.Packaging.Signing;
using System.Reflection;

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
                                 customer.AddressTypeId,
                                 customer.RelationshipId,
                                 customer.OtherRelationshipId,
                                 customer.AlternatePhone,
                                 customer.Address,
                                 customer.Zip,
                                 customer.StateId,
                                 customer.ChildrenId,
                                 customer.ChildrenUnderAgeId,
                                 customer.HonoreeAge,
                                 customer.HeardResourceId,
                                 customer.SpecifyOther,
                                 customer.Comments,
                                 EventDate = eventInfo.EventInfoEventDate.HasValue
                                          ? (DateTime?)eventInfo.EventInfoEventDate.Value.ToDateTime(new TimeOnly(0, 0))
                                          : (DateTime?)null,
                                 eventInfo.ContractEventInfoId,
                                 eventInfo.EventInfoEventType,
                                 eventInfo.EventInfoNumberOfChildren,
                                 EventInfoEventDate = eventInfo.EventInfoEventDate,
                                 eventInfo.EventInfoPartyStartTime,
                                 eventInfo.EventInfoPartyEndTime,
                                 eventInfo.EventInfoTeamAssigned,
                                 eventInfo.EventInfoStartClownHour,
                                 eventInfo.EventInfoEndClownHour,
                                 eventInfo.EventInfoEventAddress,
                                 eventInfo.EventInfoEventCity,
                                 eventInfo.EventInfoEventZip,
                                 eventInfo.EventInfoEventState,
                                 eventInfo.EventInfoVenue,
                                 eventInfo.EventInfoVenueDescription,
                                 timeTeamInfo.ContractNo,
                                 timeTeamInfo.ContractId,
                                 ContractDate = timeTeamInfo.EntryDate,
                                 partyPackage.PartyPackageName,
                                 contractBookingPayment.PaymentStatusId,
                                 timeTeamInfo.ContractStatusId,
                                 customer.CustomerId,
                                 contractPackage.PartyPackageId,
                                 contractPackage.CategoryId,
                                 contractPackage.PackageInfoId,
                                 contractPackage.Price,
                                 contractPackage.Tax,
                                 contractPackage.Tip,
                                 contractPackage.Description,
                                 Characters = (from a in _context.ContractPackageInfoCharacters
                                               where a.PackageInfoId == contractPackage.PackageInfoId
                                               select a).ToList(),
                                 Addons = (from a in _context.ContractPackageInfoAddons
                                               where a.PackageInfoId == contractPackage.PackageInfoId
                                               select a).ToList(),
                                 Bounces = (from a in _context.ContractPackageInfoBounces
                                               where a.PackageInfoId == contractPackage.PackageInfoId
                                               select a).ToList(),
                                 contractPackage.ParkingFees,
                                 contractPackage.TollFees,
                                 contractPackage.Deposit,
                                 contractPackage.Tip2,
                                 contractPackage.Subtract,
                                 contractPackage.TotalBalance,
                                 BookingPaymentInfoId  = contractBookingPayment.BookingPaymentInfoId == null? 0 : contractBookingPayment.BookingPaymentInfoId,
                                 CardNumber = contractBookingPayment.CardNumber == null? "" : contractBookingPayment.CardNumber,
                                 CardTypeId= contractBookingPayment.CardTypeId == null? 0 : contractBookingPayment.CardTypeId,
                                 ExpireMonthYear = contractBookingPayment.ExpireMonthYear == null? "" : contractBookingPayment.ExpireMonthYear,
                                 Cvv = contractBookingPayment.Cvv == null? 0 : contractBookingPayment.Cvv,
                                 CardNumber2 = contractBookingPayment.CardNumber2 == null? "" : contractBookingPayment.CardNumber2,
                                 CardTypeId2 = contractBookingPayment.CardTypeId2 == null? 0  : contractBookingPayment.CardTypeId2,
                                 ExpireMonthYear2 = contractBookingPayment.ExpireMonthYear2 == null? "" : contractBookingPayment.ExpireMonthYear2,
                                 Cvv2 = contractBookingPayment.Cvv2 == null? 0 : contractBookingPayment.Cvv2,
                                 PaymentStatus = contractBookingPayment.PaymentStatusId == null? 0 : contractBookingPayment.PaymentStatusId,
                                 BillingAddress = contractBookingPayment.BillingAddress == null? "" : contractBookingPayment.BillingAddress,
                                 UseAddress = contractBookingPayment.UseAddress == null? false : contractBookingPayment.UseAddress

                             }).AsQueryable();



                if (CustomerId !=0 && CustomerId !=0)
                    query = query.Where(x => x.CustomerId.Equals(CustomerId));

                if (ContractId != 0 && ContractId != 0)
                    query = query.Where(x => x.ContractId.Equals(ContractId));



                var test = query.ToList();
                var results = await query

                    .Select(x => new SearchResultContractDto
                    {

                        CustomerId = x.CustomerId,
                        AddressTypeId = x.AddressTypeId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        EmailAddress = x.EmailAddress, 
                        City =  x.City,
                        PhoneNo = x.PhoneNo,
                        HonoreeName = x.HonoreeName,
                        RelationshipId = x.RelationshipId,
                        OtherRelationshipId = x.OtherRelationshipId,
                        AlternatePhone = x.AlternatePhone,
                        Address = x.Address,
                        Zip = x.Zip,
                        StateId = x.StateId,
                        ChildrenId = x.ChildrenId,
                        ChildrenUnderAgeId = x.ChildrenUnderAgeId,
                        HonoreeAge = x.HonoreeAge,
                        HeardResourceId = x.HeardResourceId,
                        SpecifyOther = x.SpecifyOther,
                        Comments = x.Comments, 
                        ContractId = x.ContractId,
                        ContractNumber = x.ContractNo,
                        ContractDate = x.ContractDate,
                        EventDate = x.EventDate,
                        ContractStatusId = x.ContractStatusId,
                        ContractEventInfoId = x.ContractEventInfoId,
                        EventInfoEventType = x.EventInfoEventType,
                        EventInfoNumberOfChildren =x.EventInfoNumberOfChildren,
                        EventInfoEventDate =  x.EventInfoEventDate,
                        EventInfoPartyStartTime = x.EventInfoPartyStartTime,
                        EventInfoPartyEndTime = x.EventInfoPartyEndTime,
                        EventInfoTeamAssigned = x.EventInfoTeamAssigned,
                        EventInfoStartClownHour = x.EventInfoStartClownHour, 
                        EventInfoEndClownHour = x.EventInfoEndClownHour,
                        EventInfoEventAddress = x.EventInfoEventAddress,
                        EventInfoEventCity = x.EventInfoEventCity,
                        EventInfoEventZip = x.EventInfoEventZip,
                        EventInfoEventState = x.EventInfoEventState,
                        EventInfoVenue = x.EventInfoVenue,
                        EventInfoVenueDescription =x.EventInfoVenueDescription,
                        PartyPackageId = x.PartyPackageId,
                        CategoryId = x.CategoryId,
                        PackageInfoId = x.PackageInfoId,
                        Price = x.Price,
                        Tax = x.Tax,
                        Tip = x.Tip,
                        Description = x.Description,
                        Characters = x.Characters, 
                        Addons =  x.Addons, 
                        Bounces = x.Bounces, 
                        ParkingFees = x.ParkingFees,
                        TollFees = x.TollFees,
                        Deposit = x.Deposit,
                        Tip2 = x.Tip2,
                        Subtract = x.Subtract,
                        TotalBalance = x.TotalBalance,
                        bookingPaymentInfoId = x.BookingPaymentInfoId,

                        cardNumber1 = x.CardNumber,
                        cardType1 = x.CardTypeId,
                        expirationDate1 = x.ExpireMonthYear,
                        cvv1 = x.Cvv,
                        cardNumber2 = x.CardNumber2,
                        cardType2 = x.CardTypeId2,
                        expirationDate2 = x.ExpireMonthYear2,
                        cvv2 = x.Cvv2,
                        paymentStatus = x.PaymentStatus,
                        billingAddress = x.BillingAddress,
                        useAddress = x.UseAddress
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
