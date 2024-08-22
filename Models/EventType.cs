using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class EventType
{
    public int EventTypeId { get; set; }

    public string EventTypeName { get; set; } = null!;
}
