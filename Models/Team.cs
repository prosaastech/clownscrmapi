using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamNo { get; set; } = null!;

    public virtual ICollection<ContractTimeTeamInfo> ContractTimeTeamInfos { get; set; } = new List<ContractTimeTeamInfo>();
}
