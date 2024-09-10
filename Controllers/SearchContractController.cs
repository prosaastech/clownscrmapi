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

                //var query = (from customer in _context.CustomerInfos

                //                 // Left join with ContractEventInfos
                //             join eventInfo in _context.ContractEventInfos
                //             on customer.CustomerId equals (int?)eventInfo.CustomerId into customerEvents
                //             from eventInfo in customerEvents.DefaultIfEmpty()

                //                 // Left join with ContractTimeTeamInfos (using composite keys)
                //             join timeTeamInfo in _context.ContractTimeTeamInfos
                //             on new { CustomerId = (int?)customer.CustomerId, ContractId = (int?)eventInfo.ContractId, BranchId = (int?)customer.BranchId, CompanyId = (int?)customer.CompanyId }
                //             equals new { CustomerId = (int?)timeTeamInfo.CustomerId, ContractId = (int?)timeTeamInfo.ContractId, BranchId = (int?)timeTeamInfo.BranchId, CompanyId = (int?)timeTeamInfo.CompanyId } into eventTimes
                //             from timeTeamInfo in eventTimes.DefaultIfEmpty()

                //                 // Left join with States
                //             join state in _context.States
                //             on customer.StateId equals (int?)state.StateId into stateInfo
                //             from state in stateInfo.DefaultIfEmpty()

                //                 // Left join with Contract_PackageInfo based on CustomerId and ContractId
                //             join contractPackage in _context.ContractPackageInfos
                //             on new { CustomerId = (int?)customer.CustomerId, ContractId = (int?)timeTeamInfo.ContractId }
                //             equals new { CustomerId = (int?)contractPackage.CustomerId, ContractId = (int?)contractPackage.ContractId } into contractPackages
                //             from contractPackage in contractPackages.DefaultIfEmpty()

                //                 // Left join with PartyPackage based on PartyPackageId from Contract_PackageInfo
                //             join partyPackage in _context.PartyPackages
                //             on (int?)contractPackage.PartyPackageId equals (int?)partyPackage.PartyPackageId into partyPackageInfo
                //             from partyPackage in partyPackageInfo.DefaultIfEmpty()

                //                 // Where clause to filter by BranchId and CompanyId
                //             where customer.BranchId == branchId && customer.CompanyId == companyId

                //             // Select fields
                //             select new
                //             {
                //                 customer.FirstName,
                //                 customer.LastName,
                //                 customer.EmailAddress,
                //                 customer.City,
                //                 customer.PhoneNo,
                //                 customer.HonoreeName,

                //                 // State information
                //                 StateId = state != null ? state.StateId : (int?)null,
                //                 StateName = state != null ? state.StateName : null,

                //                 // Event information
                //                 EventDate = eventInfo != null
                //                            ? (eventInfo.EventInfoEventDate.HasValue
                //                                ? (DateTime?)eventInfo.EventInfoEventDate.Value.ToDateTime(new TimeOnly(0, 0))
                //                                : (DateTime?)null)
                //                            : (DateTime?)null,

                //                 // Contract information
                //                 ContractNo = timeTeamInfo != null ? timeTeamInfo.ContractNo : null,
                //                 ContractId = timeTeamInfo != null ? timeTeamInfo.ContractId : null,
                //                 ContractDate = timeTeamInfo != null ? timeTeamInfo.EntryDate : null,

                //                 // Package information
                //                 PackageId = contractPackage != null ? contractPackage.PartyPackageId : (int?)null,
                //                 PackageName = partyPackage != null ? partyPackage.PartyPackageName : null
                //             }).AsQueryable();
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

                                 // Where clause to filter by BranchId and CompanyId
                             where customer.BranchId == branchId && customer.CompanyId == companyId

                             // Group results to aggregate character, bounce, and addon names
                             group new { customer, eventInfo, timeTeamInfo, state, contractPackage, partyPackage, character, bounce, addon }
                             by new
                             {
                                 customer.FirstName,
                                 customer.LastName,
                                 customer.EmailAddress,
                                 customer.City,
                                 customer.PhoneNo,
                                 customer.HonoreeName,
                                 state.StateId,
                                 state.StateName,
                                 eventInfo.EventInfoEventDate,
                                 timeTeamInfo.ContractNo,
                                 timeTeamInfo.ContractId,
                                 timeTeamInfo.EntryDate,
                                 contractPackage.PartyPackageId,
                                 partyPackage.PartyPackageName
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

                                 // Concatenated bounce names
                                 Bounces = string.Join(", ", grouped.Select(g => g.bounce != null ? g.bounce.BounceName : "").Where(name => !string.IsNullOrEmpty(name)).Distinct()),

                                 // Concatenated addon names
                                 Addons = string.Join(", ", grouped.Select(g => g.addon != null ? g.addon.AddonName : "").Where(name => !string.IsNullOrEmpty(name)).Distinct())
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
                        EmailAddress = x.EmailAddress,
                        ContractId = x.ContractId,
                        ContractNumber = x.ContractNo,
                        ContractDate = x.ContractDate,
                        EventDate = x.EventDate,
                        StateId = x.StateId,
                        StateName = x.StateName,
                        City = x.City,
                        PackageName = x.PackageName,
                        primaryHonoree = x.HonoreeName,
                        characters = x.Characters,
                        bounces = x.Bounces,
                        addOns = x.Addons

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


    }
}
