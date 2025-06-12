using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovision.Core.Application.Requests.Branches;

public class CompanyBranchListDto : IMapFrom<Branch>
{
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public bool IsMain { get; set; }
    public bool IsActive { get; set; }
}
