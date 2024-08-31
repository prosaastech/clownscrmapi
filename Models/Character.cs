using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class Character
{
    public int CharacterId { get; set; }

    public string CharacterName { get; set; } = null!;

    public double Price { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
