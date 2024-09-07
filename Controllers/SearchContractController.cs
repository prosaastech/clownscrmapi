using ClownsCRMAPI.CustomModels;
using ClownsCRMAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            List<SearchResultDto> results = new List<SearchResultDto>();

            try
            {

            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

                var query = from customer in _context.CustomerInfos
                            join contract in _context.ContractTimeTeamInfos
                                on customer.CustomerId equals contract.CustomerId
                            join eventInfo in _context.ContractEventInfos
                                on contract.ContractId equals eventInfo.ContractId into events
                            from eventInfo in events.DefaultIfEmpty()
                            join timeTeam in _context.ContractTimeTeamInfos
                                on contract.ContractId equals timeTeam.ContractId into timesTeams
                            from timeTeam in timesTeams.DefaultIfEmpty()
                            join packageInfo in _context.ContractPackageInfos
                                on contract.ContractId equals packageInfo.ContractId into packages
                            from packageInfo in packages.DefaultIfEmpty()
                            join addon in _context.ContractPackageInfoAddons
                                on packageInfo.PackageInfoId equals addon.PackageInfoId into addons
                            from addon in addons.DefaultIfEmpty()
                            join bounce in _context.ContractPackageInfoBounces
                                on packageInfo.PackageInfoId equals bounce.PackageInfoId into bounces
                            from bounce in bounces.DefaultIfEmpty()
                            join character in _context.ContractPackageInfoCharacters
                                on packageInfo.PackageInfoId equals character.PackageInfoId into characters
                            from character in characters.DefaultIfEmpty()
                            join paymentInfo in _context.ContractBookingPaymentInfos
                                on contract.ContractId equals paymentInfo.ContractId into payments
                            from paymentInfo in payments.DefaultIfEmpty()
                            where contract.BranchId == BranchId && contract.CompanyId == CompanyId
                            select new
                            {
                                Customer = customer,
                                Contract = contract,
                                EventInfo = eventInfo,
                                TimeTeam = timeTeam,
                                PackageInfo = packageInfo,
                                Addons = addons,
                                Bounces = bounces,
                                Characters = characters,
                                PaymentInfo = paymentInfo
                            };


                if (!string.IsNullOrEmpty(criteria.FirstName))
                query = query.Where(x => x.Customer.FirstName.Contains(criteria.FirstName));

            if (!string.IsNullOrEmpty(criteria.LastName))
                query = query.Where(x => x.Customer.LastName.Contains(criteria.LastName));

            if (!string.IsNullOrEmpty(criteria.CustomerEmail))
                query = query.Where(x => x.Customer.EmailAddress.Contains(criteria.CustomerEmail));

            if (criteria.EventDate.HasValue)
            {
                var eventDateOnly = DateOnly.FromDateTime(criteria.EventDate.Value); // Convert to DateOnly
                query = query.Where(x => x.EventInfo.EventInfoEventDate.HasValue &&
                                         x.EventInfo.EventInfoEventDate.Value == eventDateOnly);
            }

                // Add more conditions for other fields as needed

               results = await query
                    .Select(x => new SearchResultDto
                    {
                        Customer = x.Customer,
                        Contract = x.Contract,
                        EventInfo = x.EventInfo,
                        TimeTeam = x.TimeTeam,
                        PackageInfo = x.PackageInfo,
                        //Addons = x.Addons.ToList(), 
                        //Bounces = x.Bounces.ToList(),
                        //Characters = x.Characters.ToList(),
                        PaymentInfo = x.PaymentInfo
                    }).ToListAsync();


            }
            catch (Exception ex)
            {

                 
            }
            return Ok(results);
        }

    }
}
