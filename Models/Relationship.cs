using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class Relationship
{
    public int RelationshipId { get; set; }

    public string RelationshipName { get; set; } = null!;
}
