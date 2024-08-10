﻿using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? IsActive { get; set; }
}
