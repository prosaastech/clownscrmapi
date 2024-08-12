﻿using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class ContractTimeTeamInfo
{
    public long ContractTimeTeamInfoId { get; set; }

    public int TimeId { get; set; }

    public int TimeSlotId { get; set; }

    public int? ContractId { get; set; }

    public DateOnly Date { get; set; }

    public virtual Contract? Contract { get; set; }

    public virtual Team Time { get; set; } = null!;

    public virtual TimeSlot TimeSlot { get; set; } = null!;
}
