﻿namespace ClownsCRMAPI.CustomModels
{
    public class SearchCriteria
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? contractNumber { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime? EventDate { get; set; }
        public int? State { get; set; }
        public string? City { get; set; } // Added for city
        public string? PrimaryHonoree { get; set; } // Added for primary honoree
        public int? Category { get; set; }
        public int? PartyPackage { get; set; }
        public int? Characters { get; set; }
        public int? Bounces { get; set; }
        public int? AddOns { get; set; }
        public int? Venue { get; set; }
        public int? PaymentStatus { get; set; }

         public bool Approval { get; set; }
        public bool Confirmation { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }


}
