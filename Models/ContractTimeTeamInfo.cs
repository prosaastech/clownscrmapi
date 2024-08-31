using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class ContractTimeTeamInfo
{
    public long ContractTimeTeamInfoId { get; set; }

    public int TeamId { get; set; }

    public int TimeSlotId { get; set; }

    public int? ContractId { get; set; }

    public DateOnly Date { get; set; }

    public int? CustomerId { get; set; }

    public DateOnly? EntryDate { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual TimeSlot TimeSlot { get; set; } = null!;
}
