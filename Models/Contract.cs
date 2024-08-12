using System;
using System.Collections.Generic;

namespace ClownsCRMAPI.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public virtual ICollection<ContractTimeTeamInfo> ContractTimeTeamInfos { get; set; } = new List<ContractTimeTeamInfo>();
}
