using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class Addon
{
    public int AddonId { get; set; }

    public string AddonName { get; set; } = null!;

    public double Price { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
