﻿using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class Child
{
    public int ChildrenId { get; set; }

    public int ChildrenNo { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
