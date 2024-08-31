using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamNo { get; set; } = null!;

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }

    public virtual ICollection<ContractTimeTeamInfo> ContractTimeTeamInfos { get; set; } = new List<ContractTimeTeamInfo>();
}
