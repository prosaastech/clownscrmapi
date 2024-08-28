using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.Models;
using ClownsCRMAPI.CustomModels;

namespace ClownsCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractEventInfoesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractEventInfoesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ContractEventInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractEventInfo>>> GetContractEventInfos()
        {
            return await _context.ContractEventInfos.ToListAsync();
        }

        // GET: api/ContractEventInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractEventInfo>> GetContractEventInfo(int id)
        {
            var contractEventInfo = await _context.ContractEventInfos.FindAsync(id);

            if (contractEventInfo == null)
            {
                return NotFound();
            }

            return contractEventInfo;
        }

        // PUT: api/ContractEventInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractEventInfo(int id, ContractEventInfo contractEventInfo)
        {
            if (id != contractEventInfo.ContractEventInfoId)
            {
                return BadRequest();
            }

            _context.Entry(contractEventInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractEventInfoExists(id))
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

        // POST: api/ContractEventInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("SaveEventInfo")]
        //public async Task<ActionResult<ContractEventInfo>> PostContractEventInfo(EventInfoModel model)
        //{
        //    // Convert EventInfoModel to ContractEventInfo
        //    var contractEventInfo = new ContractEventInfo
        //    {
        //        ContractEventInfoId = model.ContractEventInfoId,
        //        EventInfoEventType = model.EventInfoEventType,
        //        EventInfoNumberOfChildren = model.EventInfoNumberOfChildren,
        //        EventInfoEventDate = model.EventInfoEventDate.HasValue
        //                             ? DateOnly.FromDateTime(model.EventInfoEventDate.Value)
        //                             : null,
        //        EventInfoPartyStartTime = !string.IsNullOrEmpty(model.EventInfoPartyStartTime)
        //                                  ? TimeOnly.Parse(model.EventInfoPartyStartTime)
        //                                  : null,
        //        EventInfoPartyEndTime = !string.IsNullOrEmpty(model.EventInfoPartyEndTime)
        //                                ? TimeOnly.Parse(model.EventInfoPartyEndTime)
        //                                : null,
        //        EventInfoTeamAssigned = model.EventInfoTeamAssigned,
        //        EventInfoStartClownHour = !string.IsNullOrEmpty(model.EventInfoStartClownHour)
        //                                  ? TimeOnly.Parse(model.EventInfoStartClownHour)
        //                                  : null,
        //        EventInfoEndClownHour = !string.IsNullOrEmpty(model.EventInfoEndClownHour)
        //                                ? TimeOnly.Parse(model.EventInfoEndClownHour)
        //                                : null,
        //        EventInfoEventAddress = model.EventInfoEventAddress,
        //        EventInfoEventCity = model.EventInfoEventCity,
        //        EventInfoEventZip = model.EventInfoEventZip,
        //        EventInfoEventState = model.EventInfoEventState,
        //        EventInfoVenue = model.EventInfoVenue,
        //        EventInfoVenueDescription = model.EventInfoVenueDescription,
        //        ContractId = model.ContractId,
        //        CustomerId = model.CustomerId
        //    };

        //    // Add to context and save changes
        //    _context.ContractEventInfos.Add(contractEventInfo);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetContractEventInfo", new { id = contractEventInfo.ContractEventInfoId }, contractEventInfo);
        //}
        [HttpPost("SaveEventInfo")]
        public async Task<ActionResult<ContractEventInfo>> PostContractEventInfo(EventInfoModel model)
        {
            ContractEventInfo contractEventInfo;

            if (model.ContractEventInfoId == 0)
            {
                // Create a new ContractEventInfo record
                contractEventInfo = new ContractEventInfo();
             }
            else
            {
                // Update an existing ContractEventInfo record
                contractEventInfo = await _context.ContractEventInfos
                    .FindAsync(model.ContractEventInfoId);

                if (contractEventInfo == null)
                {
                    return NotFound(); // Return 404 if the record is not found
                }
            }

            // Map the model data to the entity (common for both create and update)
            contractEventInfo.EventInfoEventType = model.EventInfoEventType;
            contractEventInfo.EventInfoNumberOfChildren = model.EventInfoNumberOfChildren;
            contractEventInfo.EventInfoEventDate = model.EventInfoEventDate.HasValue
                ? DateOnly.FromDateTime(model.EventInfoEventDate.Value)
                : null;
            contractEventInfo.EventInfoPartyStartTime = !string.IsNullOrEmpty(model.EventInfoPartyStartTime)
                ? TimeOnly.Parse(model.EventInfoPartyStartTime)
                : null;
            contractEventInfo.EventInfoPartyEndTime = !string.IsNullOrEmpty(model.EventInfoPartyEndTime)
                ? TimeOnly.Parse(model.EventInfoPartyEndTime)
                : null;
            contractEventInfo.EventInfoTeamAssigned = model.EventInfoTeamAssigned;
            contractEventInfo.EventInfoStartClownHour = !string.IsNullOrEmpty(model.EventInfoStartClownHour)
                ? TimeOnly.Parse(model.EventInfoStartClownHour)
                : null;
            contractEventInfo.EventInfoEndClownHour = !string.IsNullOrEmpty(model.EventInfoEndClownHour)
                ? TimeOnly.Parse(model.EventInfoEndClownHour)
                : null;
            contractEventInfo.EventInfoEventAddress = model.EventInfoEventAddress;
            contractEventInfo.EventInfoEventCity = model.EventInfoEventCity;
            contractEventInfo.EventInfoEventZip = model.EventInfoEventZip;
            contractEventInfo.EventInfoEventState = model.EventInfoEventState;
            contractEventInfo.EventInfoVenue = model.EventInfoVenue;
            contractEventInfo.EventInfoVenueDescription = model.EventInfoVenueDescription;
            contractEventInfo.ContractId = model.ContractId;
            contractEventInfo.CustomerId = model.CustomerId;

            bool IsNew = false;
            if (model.ContractEventInfoId == 0)
            {
                 _context.ContractEventInfos.Add(contractEventInfo);
                IsNew = true;
            }
            // Save changes to the database
            await _context.SaveChangesAsync(); 

            if (IsNew)
            {
                await SaveContractAsync(model);
            }

            return CreatedAtAction("GetContractEventInfo", new { id = contractEventInfo.ContractEventInfoId }, contractEventInfo);
        }
        private List<string> GenerateTimeSlots(string startTime, string endTime, int intervalMinutes = 30)
        {
            var slots = new List<string>();
            var start = ParseTime(startTime);
            var end = ParseTime(endTime);

            if (start == end)
            {
                // If start and end times are the same, only add one slot
                slots.Add(FormatTime(start));
            }
            else
            {
                // Generate slots in intervals
                for (var time = start; time <= end; time = time.AddMinutes(intervalMinutes))
                {
                    slots.Add(FormatTime(time));
                    // Handle case where the interval might surpass the end time
                    if (time.AddMinutes(intervalMinutes) > end)
                    {
                        break;
                    }
                }
            }

            return slots;
        }


        private DateTime ParseTime(string timeStr)
        {
            return DateTime.ParseExact(timeStr, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
        }

        private string FormatTime(DateTime time)
        {
            return time.ToString("hh:mm tt");
        }

        private async Task SaveContractAsync(EventInfoModel eventInfoModel)
        {
            try
            {
                // Auto-generate contractId by incrementing the last contractId in the database
                int contractId = (_context.ContractTimeTeamInfos.Max(c => (int?)c.ContractId) ?? 0) + 1;

                // Generate time slots
                var timeSlots = GenerateTimeSlots(
                    eventInfoModel.EventInfoPartyStartTime,
                    eventInfoModel.EventInfoPartyEndTime
                );

                foreach (var timeSlot in timeSlots)
                {
                    var timeSlotEntity = _context.TimeSlots.FirstOrDefault(o => o.Time == timeSlot);
                    if (timeSlotEntity == null)
                    {
                        throw new Exception($"Time slot '{timeSlot}' not found in the database.");
                    }

                    var contractTimeTeamInfo = new ContractTimeTeamInfo
                    {
                        ContractId = contractId,
                        TeamId = Convert.ToInt32(eventInfoModel.EventInfoTeamAssigned),
                        CustomerId = eventInfoModel.CustomerId,
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        TimeSlotId = timeSlotEntity.TimeSlotId
                    };

                    _context.ContractTimeTeamInfos.Add(contractTimeTeamInfo);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        // DELETE: api/ContractEventInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractEventInfo(int id)
        {
            var contractEventInfo = await _context.ContractEventInfos.FindAsync(id);
            if (contractEventInfo == null)
            {
                return NotFound();
            }

            _context.ContractEventInfos.Remove(contractEventInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractEventInfoExists(int id)
        {
            return _context.ContractEventInfos.Any(e => e.ContractEventInfoId == id);
        }
    }
}
