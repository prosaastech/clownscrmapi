namespace ClownsCRMAPI.CustomModels
{
    public class SearchCriteria
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ContractNo { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime? EventDate { get; set; }
        public int? State { get; set; }
        public int? Category { get; set; }
        public int? PartyPackage { get; set; }
        public int? Characters { get; set; }
        public int? Bounces { get; set; }
        public int? AddOns { get; set; }
        public int? Venue { get; set; }
        public int? PaymentStatus { get; set; }
    }

}
