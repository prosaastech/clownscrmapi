using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class ChildrenUnderAge
{
    public int ChildrenUnderAgeId { get; set; }

    public int ChildrenUnderAgeNo { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
