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
                             join eventInfo in _context.ContractEventInfos
                             on customer.CustomerId equals eventInfo.CustomerId into customerEvents
                             from eventInfo in customerEvents.DefaultIfEmpty()
                             join timeTeamInfo in _context.ContractTimeTeamInfos
                             on new { eventInfo.CustomerId, eventInfo.ContractId, BranchId = customer.BranchId, CompanyId = customer.CompanyId }
                             equals new { timeTeamInfo.CustomerId, timeTeamInfo.ContractId, timeTeamInfo.BranchId, timeTeamInfo.CompanyId } into eventTimes
                                             from timeTeamInfo in eventTimes.DefaultIfEmpty()
                             where customer.BranchId == branchId && customer.CompanyId == companyId
                             select new
                             {
                                 customer.FirstName,
                                 customer.LastName,
                                 customer.EmailAddress,
                                 customer.PhoneNo,
                                 EventDate = eventInfo != null
                                            ? (eventInfo.EventInfoEventDate.HasValue
                                                ? (DateTime?)eventInfo.EventInfoEventDate.Value.ToDateTime(new TimeOnly(0, 0))
                                                : (DateTime?)null)
                                            : (DateTime?)null,
                                 ContractNo = timeTeamInfo != null ? timeTeamInfo.ContractNo : null,
                                 ContractId = timeTeamInfo != null ? timeTeamInfo.ContractId : null,
                                 ContractDate = timeTeamInfo != null ? timeTeamInfo.EntryDate : null,

                             }).AsQueryable();

                // Apply search criteria
                if (!string.IsNullOrEmpty(criteria.FirstName))
                    query = query.Where(x => x.FirstName.Contains(criteria.FirstName));

                if (!string.IsNullOrEmpty(criteria.LastName))
                    query = query.Where(x => x.LastName.Contains(criteria.LastName));

                if (!string.IsNullOrEmpty(criteria.CustomerEmail))
                    query = query.Where(x => x.EmailAddress.Contains(criteria.CustomerEmail));

                if (!string.IsNullOrEmpty(criteria.CustomerPhone))
                    query = query.Where(x => x.PhoneNo.Contains(criteria.CustomerPhone));

                var test = query.ToList();
                if (criteria.EventDate.HasValue)
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
                        ContractDate = x.ContractDate
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
