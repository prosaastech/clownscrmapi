using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class ContractPackageInfoBounce
{
    public int ContractPackageInfoBounceId { get; set; }

    public int PackageInfoId { get; set; }

    public int BounceId { get; set; }

    public decimal Price { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }

    public virtual ContractPackageInfo PackageInfo { get; set; } = null!;
}
