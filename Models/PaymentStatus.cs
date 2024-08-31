using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class PaymentStatus
{
    public int PaymentStatusId { get; set; }

    public string PaymentStatusName { get; set; } = null!;

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
