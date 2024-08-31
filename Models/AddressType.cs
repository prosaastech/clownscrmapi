using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class AddressType
{
    public int AddressTypeId { get; set; }

    public string AddressTypeName { get; set; } = null!;

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
