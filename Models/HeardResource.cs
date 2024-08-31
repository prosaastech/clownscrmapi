using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class HeardResource
{
    public int HeardResourceId { get; set; }

    public string HeardResourceName { get; set; } = null!;

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
