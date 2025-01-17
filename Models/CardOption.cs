﻿using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class CardOption
{
    public int CardOptionId { get; set; }

    public string CardOptionName { get; set; } = null!;

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
