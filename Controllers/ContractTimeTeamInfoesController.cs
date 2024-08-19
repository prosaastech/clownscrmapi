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
    public class ContractTimeTeamInfoesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractTimeTeamInfoesController(ClownsContext context)
        {
            _context = context;
        }
        [HttpGet("getAllContractsDateWise")]
        public async Task<ActionResult<object>> GetContractTimeTeamInfos(DateTime? date)
        {
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
            var listOfContractTimeInfo = await query.ToListAsync();

            // Extract teams and time slots
            var teams = listOfContractTimeInfo
                .Select(ctti => new { ctti.Team.TeamId, ctti.Team.TeamNo })
                .Distinct()
                .ToList();

            var timeSlots = listOfContractTimeInfo
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
            var listOfContractTimeInfo = await _context.ContractTimeTeamInfos
                .Include(ctti => ctti.TimeSlot)
                .Include(ctti => ctti.Team)
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
