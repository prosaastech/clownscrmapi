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
        public int? CustomerId { get; set; }
        public int? AddressTypeId { get; set; }

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


        public string? PhoneNo { get; set; }
        public string? HonoreeName { get; set; }

        public int? RelationshipId { get; set; }
        public int? OtherRelationshipId { get; set; }

        public string? AlternatePhone { get; set; }

        public string? Address { get; set; }

        public int? Zip { get; set; }

        public int? ChildrenId { get; set; }

        public int? ChildrenUnderAgeId { get; set; }
        public int? HonoreeAge { get; set; }

        public int? HeardResourceId { get; set; }

        public string? SpecifyOther { get; set; }
        public string? Comments { get; set; }
        public int? ContractStatusId { get; set; }

        public int? ContractEventInfoId { get; set; }

        public int? EventInfoEventType { get; set; }
        public int? EventInfoNumberOfChildren { get; set; }
         public DateOnly? EventInfoEventDate { get; set; }
        public TimeOnly? EventInfoPartyStartTime { get; set; }
        public TimeOnly? EventInfoPartyEndTime { get; set; }
        public int? EventInfoTeamAssigned { get; set; }

        public TimeOnly? EventInfoStartClownHour { get; set; }
        public TimeOnly? EventInfoEndClownHour { get; set; }
        public string? EventInfoEventAddress { get; set; }
        public string? EventInfoEventCity { get; set; }
        public int? EventInfoEventZip { get; set; }

        public int? EventInfoEventState { get; set; }
        public int? EventInfoVenue { get; set; }
        public string? EventInfoVenueDescription { get; set; }
 
    }

    public class SearchResultContractDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? EmailAddress { get; set; }

        public string? ContractNumber { get; set; }
        public int? ContractId { get; set; }
        public int? CustomerId { get; set; }
        public int? AddressTypeId { get; set; }

        public DateOnly? ContractDate { get; set; }
        public DateTime? EventDate { get; set; }

        public int? StateId { get; set; }
        public string? StateName { get; set; }

        public string? City { get; set; }
        public string? PackageName { get; set; }
        public bool? approval { get; set; }
        public bool? confirmation { get; set; }
        public string? primaryHonoree { get; set; }

        


        public string? PhoneNo { get; set; }
        public string? HonoreeName { get; set; }

        public int? RelationshipId { get; set; }
        public int? OtherRelationshipId { get; set; }

        public string? AlternatePhone { get; set; }

        public string? Address { get; set; }

        public int? Zip { get; set; }

        public int? ChildrenId { get; set; }

        public int? ChildrenUnderAgeId { get; set; }
        public int? HonoreeAge { get; set; }

        public int? HeardResourceId { get; set; }

        public string? SpecifyOther { get; set; }
        public string? Comments { get; set; }
        public int? ContractStatusId { get; set; }

        public int? ContractEventInfoId { get; set; }

        public int? EventInfoEventType { get; set; }
        public int? EventInfoNumberOfChildren { get; set; }
        public DateOnly? EventInfoEventDate { get; set; }
        public TimeOnly? EventInfoPartyStartTime { get; set; }
        public TimeOnly? EventInfoPartyEndTime { get; set; }
        public int? EventInfoTeamAssigned { get; set; }

        public TimeOnly? EventInfoStartClownHour { get; set; }
        public TimeOnly? EventInfoEndClownHour { get; set; }
        public string? EventInfoEventAddress { get; set; }
        public string? EventInfoEventCity { get; set; }
        public int? EventInfoEventZip { get; set; } 
        public int? EventInfoEventState { get; set; }
        public int? EventInfoVenue { get; set; }
        public string? EventInfoVenueDescription { get; set; }


        public int? PackageInfoId { get; set; }
        public int? CategoryId { get; set; }
        public int? PartyPackageId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Tip { get; set; }
        public string? Description { get; set; }
        public List<Character> Characters { get; set; }
        public List<AddonModel>? Addons { get; set; }
        public List<BounceModel>? Bounces { get; set; }
        public decimal? ParkingFees { get; set; }
        public decimal? TollFees { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? Tip2 { get; set; }
        public decimal? Subtract { get; set; }
        public decimal? TotalBalance { get; set; }
         
    }
}
