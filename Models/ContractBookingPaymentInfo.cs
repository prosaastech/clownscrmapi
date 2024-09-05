using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class ContractBookingPaymentInfo
{
    public int BookingPaymentInfoId { get; set; }

    public int? CustomerId { get; set; }

    public int? ContractId { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }

    public int? CardTypeId { get; set; }

    public string? ExpireMonthYear { get; set; }

    public int? Cvv { get; set; }

    public int? CardTypeId2 { get; set; }

    public string? ExpireMonthYear2 { get; set; }

    public int? Cvv2 { get; set; }

    public int? PaymentStatusId { get; set; }

    public string? BillingAddress { get; set; }

    public bool? UseAddress { get; set; }

    public string? CardNumber { get; set; }

    public string? CardNumber2 { get; set; }
}
