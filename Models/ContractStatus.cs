using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class ContractStatus
{
    public int ContractStatusId { get; set; }

    public string? ContractStatusName { get; set; }

    public string? ContractStatusColor { get; set; }
}
