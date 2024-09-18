using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.Models;
using ClownsCRMAPI.CustomModels;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore.Storage; // For transaction

namespace ClownsCRMAPI.Controllers
{
    [Authorize] 
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
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

            ContractEventInfo contractEventInfo;


            model.EventInfoPartyStartTime = ConvertTo12HourFormat(model.EventInfoPartyStartTime);
            model.EventInfoPartyEndTime = ConvertTo12HourFormat(model.EventInfoPartyEndTime);
            if (!string.IsNullOrEmpty(model.EventInfoStartClownHour))
            {
                model.EventInfoStartClownHour = ConvertTo12HourFormat(model.EventInfoStartClownHour);
            }
            if (!string.IsNullOrEmpty(model.EventInfoEndClownHour))
            {
                model.EventInfoEndClownHour = ConvertTo12HourFormat(model.EventInfoEndClownHour);
            }

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
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
                    contractEventInfo.BranchId = BranchId;
                    contractEventInfo.CompanyId = CompanyId;

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
                        contractEventInfo.ContractId = globalContractId;
                        _context.ContractEventInfos.Update(contractEventInfo);
                        await _context.SaveChangesAsync();

                    }
                    await transaction.CommitAsync();

                    return CreatedAtAction("GetContractEventInfo", new { id = contractEventInfo.ContractEventInfoId }, contractEventInfo);
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any error occurs
                    await transaction.RollbackAsync();
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return StatusCode(500, "An error occurred while saving event info.");
                }
            }
        }

        private async Task SaveContractAsync(EventInfoModel eventInfoModel)
        {
            int BranchId = TokenHelper.GetBranchId(HttpContext);
            int CompanyId = TokenHelper.GetCompanyId(HttpContext);

            try
            {
                // Auto-generate contractId by incrementing the last contractId in the database
                int contractId = (_context.ContractTimeTeamInfos.Max(c => (int?)c.ContractId) ?? 0) + 1;
                globalContractId = contractId;
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
                        Date = DateOnly.FromDateTime((eventInfoModel.selectedDate == DateTime.MinValue? Convert.ToDateTime( eventInfoModel.EventInfoEventDate) : eventInfoModel.selectedDate)),
                        TimeSlotId = timeSlotEntity.TimeSlotId,
                        EntryDate = DateOnly.FromDateTime(DateTime.Now),
                        BranchId = BranchId,
                        CompanyId = CompanyId,
                        ContractNo = GenerateContractNumber(contractId)
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


        public static string GenerateContractNumber(int _counter)
        {
            // Get the current date in the format DDMMYYYY
            string datePart = DateTime.Now.ToString("ddMMyyyy");

            // Generate a unique identifier (e.g., counter or random number)
            // You can increment the counter or use something more sophisticated, such as a database ID.
            string uniquePart = "N" + _counter.ToString("D6"); // N followed by a 6-digit number, padded with zeroes

            // Combine the date and unique identifier
            string contractNumber = $"{datePart}{uniquePart}";

            // Increment the counter for the next contract
            _counter++;

            return contractNumber;
        }
        public static string ConvertTo12HourFormat(string time)
        {
            // Check if the input time already contains "AM" or "PM"
            if (time.Contains("AM", StringComparison.OrdinalIgnoreCase) || time.Contains("PM", StringComparison.OrdinalIgnoreCase))
            {
                return time; // Return the same time if it already contains AM/PM
            }

            // Parse the time string to a DateTime object
            DateTime parsedTime;
            if (DateTime.TryParse(time, out parsedTime))
            {
                // Convert to 12-hour format with AM/PM
                return parsedTime.ToString("hh:mm tt");
            }
            else
            {
                // Return an error message if parsing fails
                return "Invalid time format";
            }
        }
        int globalContractId = 0;
      
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
