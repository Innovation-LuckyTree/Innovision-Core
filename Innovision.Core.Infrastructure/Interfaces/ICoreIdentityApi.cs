using Innovision.Core.Infrastructure.CoreIdentity.Models.Requests;
using Innovision.Core.Infrastructure.CoreIdentity.Models.Responses;

namespace Innovision.Core.Infrastructure.Interfaces
{
    public interface ICoreIdentityApi
    {
        Task<LoginUserResponse> LoginUser(string userName, string password, string ipAddress, CancellationToken cancellationToken);
        Task<CreateUserResponse> CreateUserIdentity(CreateUserRequest request, CancellationToken cancellationToken);
        Task UpdateUserPassword(UpdateUserPasswordRequest request, CancellationToken cancellationToken);
        Task<LockedUsersResponse> GetLockedUsers(Guid CompanyObjectId, int? PageNumber, int? PageSize, CancellationToken cancellationToken);
        Task<bool> GetLockedUser(Guid UserId, CancellationToken cancellationToken);
    }
}
