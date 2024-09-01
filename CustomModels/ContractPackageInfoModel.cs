using ClownsCRMAPI.Models;
using System.Collections.Generic;

namespace ClownsCRMAPI.CustomModels
{
    public class ContractPackageInfoModel
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

        public int? BranchId { get; set; }

        public int? CompanyId { get; set; }

        public  List<Addon> Addons { get; set; } = new List<Addon>();

        public  List<Bounce> Bounces { get; set; } = new List<Bounce>();

        public  List<Character> Characters { get; set; } = new List<Character>();
    }
}
