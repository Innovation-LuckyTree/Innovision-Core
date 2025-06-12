using Innovision.Core.Application.Requests.Branches.Commands.CreateBranch;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovision.Core.Application.Common.Interfaces;

public interface IBranchServices
{
    public Branch GenerateCreateBranchModel(Details details, string branchName, Account branchOperator, bool IsActive, bool IsMain, ICurrentUserService _currentUserService);
}
