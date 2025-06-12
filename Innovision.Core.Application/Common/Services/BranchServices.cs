using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Common.Services;

public class BranchServices : IBranchServices
{
    public Branch GenerateCreateBranchModel(Details details, string branchName, Account branchOperator, bool IsActive, bool IsMain, ICurrentUserService _currentUserService)
    {
        return new Branch
        {
            BranchName = branchName,
            Region = details.Region,
            Province = details.Province,
            Municipality = details.Municipality,
            Barangay = details.Barangay,
            StreetOrPurok = details.StreetOrPurok,
            PermanentRegion = details.Region,
            PermanentProvince = details.Province,
            PermanentMunicipality = details.Municipality,
            PermanentBarangay = details.Barangay,
            PermanentStreetOrPurok = details.StreetOrPurok,
            IsActive = IsActive,
            IsMain = IsMain,
            CreatedBy = string.IsNullOrEmpty(_currentUserService.UserId) ? "System" : _currentUserService.UserId,
            ModifiedBy = string.IsNullOrEmpty(_currentUserService.UserId) ? "System" : _currentUserService.UserId,
            Account = new[] { branchOperator }
        };
    }
}
