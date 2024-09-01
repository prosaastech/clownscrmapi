using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class ContractPackageInfoCharacter
{
    public int ContractPackageInfoCharacterId { get; set; }

    public int PackageInfoId { get; set; }

    public string? CharacterId { get; set; }

    public decimal? Price { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }

    public virtual ContractPackageInfo PackageInfo { get; set; } = null!;
}
