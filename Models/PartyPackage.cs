﻿using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class PartyPackage
{
    public int PartyPackageId { get; set; }

    public string PartyPackageName { get; set; } = null!;
}