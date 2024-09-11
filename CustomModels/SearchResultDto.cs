using ClownsCRMAPI.Models;

namespace ClownsCRMAPI.CustomModels
{
    public class SearchResultDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? EmailAddress { get; set; }

        public string? ContractNumber { get; set; }
        public int? ContractId { get; set; }

        public DateOnly? ContractDate { get; set; }
        public DateTime? EventDate { get; set; }

        public int? StateId { get; set; }
        public string? StateName { get; set; }

        public string? City { get; set; }
        public string? PackageName { get; set; }
        public bool? approval { get; set; }
        public bool? confirmation { get; set; }
        public string? primaryHonoree { get; set; }

        public string? characters { get; set; }

        public string? bounces { get; set; }

        public string? addOns { get; set; }

        public int? ContractStatusId { get; set; }

        //public CustomerInfo Customer { get; set; }
        //public ContractTimeTeamInfo Contract { get; set; }
        //public ContractEventInfo EventInfo { get; set; }
        //public ContractTimeTeamInfo TimeTeam { get; set; }
        //public ContractPackageInfo PackageInfo { get; set; }
        //public IEnumerable<ContractPackageInfoAddon> Addons { get; set; }
        //public IEnumerable<ContractPackageInfoBounce> Bounces { get; set; }
        //public IEnumerable<ContractPackageInfoCharacter> Characters { get; set; }
        //public ContractBookingPaymentInfo PaymentInfo { get; set; }
    }

}
