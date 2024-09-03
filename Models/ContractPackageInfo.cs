using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class ContractPackageInfo
{
    public int PackageInfoId { get; set; }

    public int? CategoryId { get; set; }

    public int? PartyPackageId { get; set; }

    public decimal? Price { get; set; }

    public decimal? Tax { get; set; }

    public decimal? Tip { get; set; }

    public string? Description { get; set; }

    public decimal? ParkingFees { get; set; }

    public decimal? TollFees { get; set; }

    public decimal? Deposit { get; set; }

    public decimal? Tip2 { get; set; }

    public decimal? Subtract { get; set; }

    public decimal? TotalBalance { get; set; }

    public int? ContractId { get; set; }

    public int? CustomerId { get; set; }

    public int BranchId { get; set; }

    public int CompanyId { get; set; }

    public virtual ICollection<ContractPackageInfoAddon> ContractPackageInfoAddons { get; set; } = new List<ContractPackageInfoAddon>();

    public virtual ICollection<ContractPackageInfoBounce> ContractPackageInfoBounces { get; set; } = new List<ContractPackageInfoBounce>();

    public virtual ICollection<ContractPackageInfoCharacter> ContractPackageInfoCharacters { get; set; } = new List<ContractPackageInfoCharacter>();
}
