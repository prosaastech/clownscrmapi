using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class CustomerInfo
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }

    public string? PhoneNo { get; set; }

    public int? RelationshipId { get; set; }

    public int? OtherRelationshipId { get; set; }

    public string? AlternatePhone { get; set; }

    public string? Address { get; set; }

    public int? AddressTypeId { get; set; }

    public string? City { get; set; }

    public int? Zip { get; set; }

    public int? StateId { get; set; }

    public int? ChildrenId { get; set; }

    public int? ChildrenUnderAgeId { get; set; }

    public string? HonoreeName { get; set; }

    public int? HonoreeAge { get; set; }

    public int? HeardResourceId { get; set; }

    public string? Comments { get; set; }

    public string? SpecifyOther { get; set; }
}
