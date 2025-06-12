using Innovision.Core.Application.Requests.Users.Queries;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Common.Interfaces
{
    public interface ILoadingAccountServices
    {
        (List<UserControl>?, List<UserControl>?) GetAccessControl(UserTypeConfig? userTypeConfig);
        Task<List<SystemUserDto>> GetUsersList(List<UserControl>? UserControls, int BranchId, bool serviceProvider, bool isBranchUser, int userType, CancellationToken cancellationToken);
        Task<SystemUserVm> GetUsersListPaginate(List<UserControl>? UserControls, int branchId, bool serviceProvider, PagedQuery PagedQuery, Guid UserObjId, int? levelType, CancellationToken cancellationToken);
    }
}
