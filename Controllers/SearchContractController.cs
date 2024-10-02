using ClownsCRMAPI.CustomModels;
using ClownsCRMAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace ClownsCRMAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchContractController : ControllerBase
    {
        private readonly ClownsContext _context;

        public SearchContractController(ClownsContext context)
        {
            _context = context;
        }
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] SearchCriteria criteria)
        {
            int page = criteria.Page; // Make sure `SearchCriteria` has a `Page` property
            int pageSize = criteria.PageSize; // Make sure `SearchCriteria` has a `PageSize` property

            try
            {
                page = page == 0 ? 1 : page;
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

                             // Group results  to aggregate character, bounce, and addon names
                             group new { customer, eventInfo, timeTeamInfo, state, contractPackage, partyPackage, character, bounce, addon, contractBookingPayment }
                             by new 
                             {
                                 customer.FirstName,
                                 customer.LastName,
                                 EmailAddress = customer.EmailAddress == null? "" : customer.EmailAddress,
                                 City = customer.City == null? "" : customer.City,
                                 PhoneNo = customer.PhoneNo == null ? "" : customer.PhoneNo,
                                 HonoreeName = customer.HonoreeName == null ? "" : customer.HonoreeName,
                                 StateId = state.StateId == null ? 0 : state.StateId,
                                 StateName = state.StateName == null ? "" : state.StateName,
                                 eventInfo.EventInfoEventDate,
                                 timeTeamInfo.ContractNo,
                                 timeTeamInfo.ContractId,
                                 timeTeamInfo.EntryDate,
                                 contractPackage.PartyPackageId,
                                 partyPackage.PartyPackageName,
                                 contractPackage.CategoryId,
                                 eventInfo.EventInfoVenue,
                                 contractBookingPayment.PaymentStatusId,
                                 timeTeamInfo.ContractStatusId,
                                 customer.CustomerId
 
                             } into grouped

                             // Select the fields and concatenate character, bounce, and addon names
                             select new
                             {
                                 FirstName = grouped.Key.FirstName,
                                 LastName = grouped.Key.LastName,
                                 EmailAddress = grouped.Key.EmailAddress,
                                 City = grouped.Key.City,
                                 PhoneNo = grouped.Key.PhoneNo,
                                 HonoreeName = grouped.Key.HonoreeName,

                                 // State information
                                 StateId = grouped.Key.StateId,
                                 StateName = grouped.Key.StateName,

                                 // Event information
                                 EventDate = grouped.Key.EventInfoEventDate.HasValue
                                            ? (DateTime?)grouped.Key.EventInfoEventDate.Value.ToDateTime(new TimeOnly(0, 0))
                                            : (DateTime?)null,

                                 // Contract information
                                 ContractNo = grouped.Key.ContractNo,
                                 ContractId = grouped.Key.ContractId,
                                 ContractDate = grouped.Key.EntryDate,

                                 // Package information
                                 PackageId = grouped.Key.PartyPackageId,
                                 PackageName = grouped.Key.PartyPackageName,

                                 // Concatenated character names
                                 Characters = string.Join(", ", grouped.Select(g => g.character != null ? g.character.CharacterName : "").Where(name => !string.IsNullOrEmpty(name)).Distinct()),
                                 CharacterId = string.Join(", ", grouped.Select(g => g.character != null ? g.character.CharacterId.ToString() : "").Distinct().Where(id => !string.IsNullOrEmpty(id))),

                                 // Concatenated bounce names
                                 Bounces = string.Join(", ", grouped.Select(g => g.bounce != null ? g.bounce.BounceName : "").Where(name => !string.IsNullOrEmpty(name)).Distinct()),
                                 BounceId = string.Join(", ", grouped.Select(g => g.bounce != null ? g.bounce.BounceId.ToString() : "").Where(id => !string.IsNullOrEmpty(id)).Distinct()),

                                 // Concatenated addon names
                                 Addons = string.Join(", ", grouped.Select(g => g.addon != null ? g.addon.AddonName : "").Where(name => !string.IsNullOrEmpty(name)).Distinct()),
                                 AddonId = string.Join(", ", grouped.Select(g => g.addon != null ? g.addon.AddonId.ToString() : "").Where(id => !string.IsNullOrEmpty(id)).Distinct()),

                                 CategoryId = grouped.Key.CategoryId,
                                 EventInfoVenue = grouped.Key.EventInfoVenue,
                                 PaymentStatusId = grouped.Key.PaymentStatusId,
                                 ContractStatusId = grouped.Key.ContractStatusId,
                                 Confirmation = grouped.Key.ContractStatusId == 1? true : false,
                                 Approval = grouped.Key.ContractStatusId == 2? true : false,
                                 CustomerId = grouped.Key.CustomerId
                                  
                             }).AsQueryable();


                var test123 = query.ToList();

                // Apply search criteria
                if (!string.IsNullOrEmpty(criteria.FirstName))
                    query = query.Where(x => x.FirstName.ToLower().Contains(criteria.FirstName.ToLower()));

                if (!string.IsNullOrEmpty(criteria.LastName))
                    query = query.Where(x => x.LastName.Contains(criteria.LastName));

                if (!string.IsNullOrEmpty(criteria.CustomerEmail))
                    query = query.Where(x => x.EmailAddress.Contains(criteria.CustomerEmail));

                if (!string.IsNullOrEmpty(criteria.CustomerPhone))
                    query = query.Where(x => x.PhoneNo.Contains(criteria.CustomerPhone));

                if (!string.IsNullOrEmpty(criteria.contractNumber))
                    query = query.Where(x => x.ContractNo.Contains(criteria.contractNumber));

                if (criteria.State !=0 && criteria.State !=null)
                    query = query.Where(x => x.StateId.Equals(criteria.State));

                if (!string.IsNullOrEmpty(criteria.City))
                    query = query.Where(x => x.City.Contains(criteria.City));

                if (!string.IsNullOrEmpty(criteria.PrimaryHonoree))
                    query = query.Where(x => x.HonoreeName.Contains(criteria.PrimaryHonoree));

                if (criteria.Category != 0 && criteria.Category != null)
                    query = query.Where(x => x.CategoryId.Equals(criteria.Category));

                if (criteria.PartyPackage != 0 && criteria.PartyPackage != null)
                    query = query.Where(x => x.PackageId.Equals(criteria.PartyPackage));

                if (criteria.Venue != 0 && criteria.Venue != null)
                    query = query.Where(x => x.EventInfoVenue.Equals(criteria.Venue));

                if (criteria.PaymentStatus != 0 && criteria.PaymentStatus != null)
                    query = query.Where(x => x.PaymentStatusId.Equals(criteria.PaymentStatus));


                query = query.Where(x => x.Approval.Equals(criteria.Approval));
                query = query.Where(x => x.Confirmation.Equals(criteria.Confirmation));

                var test1234 = query.ToList();

                if (criteria.Characters.HasValue && criteria.Characters !=0)
                {
                    query = query.Where(x => x.CharacterId.Contains(criteria.Characters.ToString()));
                }

                if (criteria.Bounces.HasValue && criteria.Bounces !=0)
                {
                    query = query.Where(x => x.BounceId.Contains(criteria.Bounces.ToString()));
                }

                if (criteria.AddOns.HasValue && criteria.AddOns !=0)
                {
                    query = query.Where(x => x.AddonId.Contains(criteria.AddOns.ToString()));
                }
                var test = query.ToList();
                if (criteria.EventDate.HasValue && criteria.EventDate.ToString() != "1/01/1009 12:00:00 AM")
                {
                    var eventDate = criteria.EventDate.Value.Date; // Ensure you are working with DateTime only
                    query = query.Where(x => x.EventDate.HasValue && x.EventDate.Value.Date == eventDate); // Compare only the date part
                }

                // Get the total count of items
                var totalCount = await query.CountAsync();

                // Apply pagination
                var results = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new SearchResultDto
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        EmailAddress = x.EmailAddress == null? "" : x.EmailAddress,
                        ContractId = x.ContractId == null? 0 : x.ContractId,
                        ContractNumber = x.ContractNo == null? "" : x.ContractNo,
                        ContractDate = x.ContractDate,
                        EventDate = x.EventDate,
                        StateId = x.StateId,
                        StateName = x.StateName,
                        City = x.City,
                        PackageName = x.PackageName,
                        primaryHonoree = x.HonoreeName,
                        characters = x.Characters,
                        bounces = x.Bounces,
                        addOns = x.Addons,
                        approval = x.Approval,
                        confirmation = x.Confirmation,
                        ContractStatusId = x.ContractStatusId,
                        CustomerId = x.CustomerId

                    })
                    .ToListAsync();

                // Return paginated results
                return Ok(new
                {
                    Data = results,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize
                });
            }
            catch (Exception ex)
            {
                // Log exception (if you have a logging mechanism)
                return StatusCode(500, "Internal server error");
            }
        }


        //[HttpPost("search")]
        //public async Task<IActionResult> Search([FromBody] SearchCriteria criteria)
        //{
        //    int page = criteria.Page; // Make sure `SearchCriteria` has a `Page` property
        //    int pageSize = criteria.PageSize; // Make sure `SearchCriteria` has a `PageSize` property

        //    try
        //    {
        //        page = page == 0 ? 1 : page;
        //        int branchId = TokenHelper.GetBranchId(HttpContext);
        //        int companyId = TokenHelper.GetCompanyId(HttpContext);

        //        var query = (from customer in _context.CustomerInfos

        //                         // Left join with ContractEventInfos
        //                     join eventInfo in _context.ContractEventInfos
        //                     on customer.CustomerId equals (int?)eventInfo.CustomerId into customerEvents
        //                     from eventInfo in customerEvents.DefaultIfEmpty()

        //                         // Left join with ContractTimeTeamInfos (using composite keys)
        //                     join timeTeamInfo in _context.ContractTimeTeamInfos
        //                     on new { CustomerId = (int?)customer.CustomerId, ContractId = (int?)eventInfo.ContractId, BranchId = (int?)customer.BranchId, CompanyId = (int?)customer.CompanyId }
        //                     equals new { CustomerId = (int?)timeTeamInfo.CustomerId, ContractId = (int?)timeTeamInfo.ContractId, BranchId = (int?)timeTeamInfo.BranchId, CompanyId = (int?)timeTeamInfo.CompanyId } into eventTimes
        //                     from timeTeamInfo in eventTimes.DefaultIfEmpty()

        //                         // Left join with States
        //                     join state in _context.States
        //                     on customer.StateId equals (int?)state.StateId into stateInfo
        //                     from state in stateInfo.DefaultIfEmpty()

        //                         // Left join with Contract_PackageInfo based on CustomerId and ContractId
        //                     join contractPackage in _context.ContractPackageInfos
        //                     on new { CustomerId = (int?)customer.CustomerId, ContractId = (int?)timeTeamInfo.ContractId }
        //                     equals new { CustomerId = (int?)contractPackage.CustomerId, ContractId = (int?)contractPackage.ContractId } into contractPackages
        //                     from contractPackage in contractPackages.DefaultIfEmpty()

        //                         // Left join with PartyPackage based on PartyPackageId from Contract_PackageInfo
        //                     join partyPackage in _context.PartyPackages
        //                     on (int?)contractPackage.PartyPackageId equals (int?)partyPackage.PartyPackageId into partyPackageInfo
        //                     from partyPackage in partyPackageInfo.DefaultIfEmpty()

        //                         // Left join with Contract_PackageInfo_Character to get related characters
        //                     join contractPackageCharacter in _context.ContractPackageInfoCharacters
        //                     on contractPackage.PackageInfoId equals contractPackageCharacter.PackageInfoId into packageCharacters
        //                     from contractPackageCharacter in packageCharacters.DefaultIfEmpty()

        //                         // Left join with Character to get character names
        //                     join character in _context.Characters
        //                     on contractPackageCharacter.CharacterId equals character.CharacterId into characterInfo
        //                     from character in characterInfo.DefaultIfEmpty()

        //                         // Left join with Contract_PackageInfo_Bounce to get related bounces
        //                     join contractPackageBounce in _context.ContractPackageInfoBounces
        //                     on contractPackage.PackageInfoId equals contractPackageBounce.PackageInfoId into packageBounces
        //                     from contractPackageBounce in packageBounces.DefaultIfEmpty()

        //                         // Left join with Bounce to get bounce names
        //                     join bounce in _context.Bounces
        //                     on contractPackageBounce.BounceId equals bounce.BounceId into bounceInfo
        //                     from bounce in bounceInfo.DefaultIfEmpty()

        //                         // Left join with Contract_PackageInfo_Addon to get related addons
        //                     join contractPackageAddon in _context.ContractPackageInfoAddons
        //                     on contractPackage.PackageInfoId equals contractPackageAddon.PackageInfoId into packageAddons
        //                     from contractPackageAddon in packageAddons.DefaultIfEmpty()

        //                         // Left join with Addon to get addon names
        //                     join addon in _context.Addons
        //                     on contractPackageAddon.AddonId equals addon.AddonId into addonInfo
        //                     from addon in addonInfo.DefaultIfEmpty()

        //                     join contractBookingPayment in _context.ContractBookingPaymentInfos
        //                     on new { CustomerId = (int?)customer.CustomerId, ContractId = (int?)timeTeamInfo.ContractId }
        //                     equals new { CustomerId = (int?)contractBookingPayment.CustomerId, ContractId = (int?)contractBookingPayment.ContractId } into contractBookingPayments
        //                     from contractBookingPayment in contractBookingPayments.DefaultIfEmpty()

        //                         // Where clause to filter by BranchId and CompanyId
        //                     where customer.BranchId == branchId && customer.CompanyId == companyId

        //                     // Group results to aggregate character, bounce, and addon names
        //                     group new { customer, eventInfo, timeTeamInfo, state, contractPackage, partyPackage, character, bounce, addon, contractBookingPayment }
        //                     by new
        //                     {
        //                         customer.FirstName,
        //                         customer.LastName,
        //                         EmailAddress = customer.EmailAddress == null ? "" : customer.EmailAddress,
        //                         City = customer.City == null ? "" : customer.City,
        //                         PhoneNo = customer.PhoneNo == null ? "" : customer.PhoneNo,
        //                         HonoreeName = customer.HonoreeName == null ? "" : customer.HonoreeName,
        //                         StateId = state.StateId == null ? 0 : state.StateId,
        //                         StateName = state.StateName == null ? "" : state.StateName,
        //                         eventInfo.EventInfoEventDate,
        //                         timeTeamInfo.ContractNo,
        //                         timeTeamInfo.ContractId,
        //                         timeTeamInfo.EntryDate,
        //                         contractPackage.PartyPackageId,
        //                         partyPackage.PartyPackageName,
        //                         contractPackage.CategoryId,
        //                         eventInfo.EventInfoVenue,
        //                         contractBookingPayment.PaymentStatusId,
        //                         timeTeamInfo.ContractStatusId,
        //                         customer.CustomerId

        //                     } into grouped

        //                     // Select the fields and concatenate character, bounce, and addon names
        //                     select new
        //                     {
        //                         FirstName = grouped.Key.FirstName,
        //                         LastName = grouped.Key.LastName,
        //                         EmailAddress = grouped.Key.EmailAddress,
        //                         City = grouped.Key.City,
        //                         PhoneNo = grouped.Key.PhoneNo,
        //                         HonoreeName = grouped.Key.HonoreeName,

        //                         // State information
        //                         StateId = grouped.Key.StateId,
        //                         StateName = grouped.Key.StateName,

        //                         // Event information
        //                         EventDate = grouped.Key.EventInfoEventDate.HasValue
        //                                    ? (DateTime?)grouped.Key.EventInfoEventDate.Value.ToDateTime(new TimeOnly(0, 0))
        //                                    : (DateTime?)null,

        //                         // Contract information
        //                         ContractNo = grouped.Key.ContractNo,
        //                         ContractId = grouped.Key.ContractId,
        //                         ContractDate = grouped.Key.EntryDate,

        //                         // Package information
        //                         PackageId = grouped.Key.PartyPackageId,
        //                         PackageName = grouped.Key.PartyPackageName,

        //                         // Concatenated character names
        //                         Characters = string.Join(", ", grouped.Select(g => g.character != null ? g.character.CharacterName : "").Where(name => !string.IsNullOrEmpty(name)).Distinct()),
        //                         CharacterId = string.Join(", ", grouped.Select(g => g.character != null ? g.character.CharacterId.ToString() : "").Distinct().Where(id => !string.IsNullOrEmpty(id))),

        //                         // Concatenated bounce names
        //                         Bounces = string.Join(", ", grouped.Select(g => g.bounce != null ? g.bounce.BounceName : "").Where(name => !string.IsNullOrEmpty(name)).Distinct()),
        //                         BounceId = string.Join(", ", grouped.Select(g => g.bounce != null ? g.bounce.BounceId.ToString() : "").Where(id => !string.IsNullOrEmpty(id)).Distinct()),

        //                         // Concatenated addon names
        //                         Addons = string.Join(", ", grouped.Select(g => g.addon != null ? g.addon.AddonName : "").Where(name => !string.IsNullOrEmpty(name)).Distinct()),
        //                         AddonId = string.Join(", ", grouped.Select(g => g.addon != null ? g.addon.AddonId.ToString() : "").Where(id => !string.IsNullOrEmpty(id)).Distinct()),

        //                         CategoryId = grouped.Key.CategoryId,
        //                         EventInfoVenue = grouped.Key.EventInfoVenue,
        //                         PaymentStatusId = grouped.Key.PaymentStatusId,
        //                         ContractStatusId = grouped.Key.ContractStatusId,
        //                         Confirmation = grouped.Key.ContractStatusId == 1 ? true : false,
        //                         Approval = grouped.Key.ContractStatusId == 2 ? true : false,
        //                         CustomerId = grouped.Key.CustomerId

        //                     }).AsQueryable();


        //        var test123 = query.ToList();

        //        // Apply search criteria
        //        if (!string.IsNullOrEmpty(criteria.FirstName))
        //            query = query.Where(x => x.FirstName.ToLower().Contains(criteria.FirstName.ToLower()));

        //        if (!string.IsNullOrEmpty(criteria.LastName))
        //            query = query.Where(x => x.LastName.Contains(criteria.LastName));

        //        if (!string.IsNullOrEmpty(criteria.CustomerEmail))
        //            query = query.Where(x => x.EmailAddress.Contains(criteria.CustomerEmail));

        //        if (!string.IsNullOrEmpty(criteria.CustomerPhone))
        //            query = query.Where(x => x.PhoneNo.Contains(criteria.CustomerPhone));

        //        if (!string.IsNullOrEmpty(criteria.contractNumber))
        //            query = query.Where(x => x.ContractNo.Contains(criteria.contractNumber));

        //        if (criteria.State != 0 && criteria.State != null)
        //            query = query.Where(x => x.StateId.Equals(criteria.State));

        //        if (!string.IsNullOrEmpty(criteria.City))
        //            query = query.Where(x => x.City.Contains(criteria.City));

        //        if (!string.IsNullOrEmpty(criteria.PrimaryHonoree))
        //            query = query.Where(x => x.HonoreeName.Contains(criteria.PrimaryHonoree));

        //        if (criteria.Category != 0 && criteria.Category != null)
        //            query = query.Where(x => x.CategoryId.Equals(criteria.Category));

        //        if (criteria.PartyPackage != 0 && criteria.PartyPackage != null)
        //            query = query.Where(x => x.PackageId.Equals(criteria.PartyPackage));

        //        if (criteria.Venue != 0 && criteria.Venue != null)
        //            query = query.Where(x => x.EventInfoVenue.Equals(criteria.Venue));

        //        if (criteria.PaymentStatus != 0 && criteria.PaymentStatus != null)
        //            query = query.Where(x => x.PaymentStatusId.Equals(criteria.PaymentStatus));


        //        query = query.Where(x => x.Approval.Equals(criteria.Approval));
        //        query = query.Where(x => x.Confirmation.Equals(criteria.Confirmation));

        //        var test1234 = query.ToList();

        //        if (criteria.Characters.HasValue && criteria.Characters != 0)
        //        {
        //            query = query.Where(x => x.CharacterId.Contains(criteria.Characters.ToString()));
        //        }

        //        if (criteria.Bounces.HasValue && criteria.Bounces != 0)
        //        {
        //            query = query.Where(x => x.BounceId.Contains(criteria.Bounces.ToString()));
        //        }

        //        if (criteria.AddOns.HasValue && criteria.AddOns != 0)
        //        {
        //            query = query.Where(x => x.AddonId.Contains(criteria.AddOns.ToString()));
        //        }
        //        var test = query.ToList();
        //        if (criteria.EventDate.HasValue && criteria.EventDate.ToString() != "1/01/1009 12:00:00 AM")
        //        {
        //            var eventDate = criteria.EventDate.Value.Date; // Ensure you are working with DateTime only
        //            query = query.Where(x => x.EventDate.HasValue && x.EventDate.Value.Date == eventDate); // Compare only the date part
        //        }

        //        // Get the total count of items
        //        var totalCount = await query.CountAsync();

        //        // Apply pagination
        //        var results = await query
        //            .Skip((page - 1) * pageSize)
        //            .Take(pageSize)
        //            .Select(x => new SearchResultDto
        //            {
        //                FirstName = x.FirstName,
        //                LastName = x.LastName,
        //                EmailAddress = x.EmailAddress == null ? "" : x.EmailAddress,
        //                ContractId = x.ContractId == null ? 0 : x.ContractId,
        //                ContractNumber = x.ContractNo == null ? "" : x.ContractNo,
        //                ContractDate = x.ContractDate,
        //                EventDate = x.EventDate,
        //                StateId = x.StateId,
        //                StateName = x.StateName,
        //                City = x.City,
        //                PackageName = x.PackageName,
        //                primaryHonoree = x.HonoreeName,
        //                characters = x.Characters,
        //                bounces = x.Bounces,
        //                addOns = x.Addons,
        //                approval = x.Approval,
        //                confirmation = x.Confirmation,
        //                ContractStatusId = x.ContractStatusId,
        //                CustomerId = x.CustomerId

        //            })
        //            .ToListAsync();

        //        // Return paginated results
        //        return Ok(new
        //        {
        //            Data = results,
        //            TotalCount = totalCount,
        //            Page = page,
        //            PageSize = pageSize
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception (if you have a logging mechanism)
        //        return StatusCode(500, "Internal server error");
        //    }
        //}


    }
}
