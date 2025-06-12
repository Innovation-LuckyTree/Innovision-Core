using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovision.Core.Application.Requests.Branches.Queries;

public record BranchVm(IEnumerable<BranchDto> Branches)
{
    public int Count
    {
        get
        {
            return Branches.Count();
        }
    }

    public int OperatorCount { get; set; }
}