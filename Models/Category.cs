using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
