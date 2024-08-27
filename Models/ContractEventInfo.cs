using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class ContractEventInfo
{
    public int ContractEventInfoId { get; set; }

    public int? EventInfoEventType { get; set; }

    public int? EventInfoNumberOfChildren { get; set; }

    public DateOnly? EventInfoEventDate { get; set; }

    public TimeOnly? EventInfoPartyStartTime { get; set; }

    public TimeOnly? EventInfoPartyEndTime { get; set; }

    public int? EventInfoTeamAssigned { get; set; }

    public TimeOnly? EventInfoStartClownHour { get; set; }

    public TimeOnly? EventInfoEndClownHour { get; set; }

    public string? EventInfoEventAddress { get; set; }

    public string? EventInfoEventCity { get; set; }

    public int? EventInfoEventZip { get; set; }

    public int? EventInfoEventState { get; set; }

    public int? EventInfoVenue { get; set; }

    public string? EventInfoVenueDescription { get; set; }

    public int? ContractId { get; set; }

    public int? CustomerId { get; set; }
}
