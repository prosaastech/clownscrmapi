using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class State
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
