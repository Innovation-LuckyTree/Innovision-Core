using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovision.Core.Application.Requests.Accounts.Users.Operator;
public class OperatorListVm
{
    public int Total { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public List<OperatorListDto> OperatorList { get; set; }

}
