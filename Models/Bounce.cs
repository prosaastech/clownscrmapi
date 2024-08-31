using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class Bounce
{
    public int BounceId { get; set; }

    public string BounceName { get; set; } = null!;

    public double Price { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
