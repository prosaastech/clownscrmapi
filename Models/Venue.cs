using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class Venue
{
    public int VenueId { get; set; }

    public string VenueName { get; set; } = null!;

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
