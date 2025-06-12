using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.AccountApproval.Commands;

public class DeclineUserCommandHandler : IRequestHandler<DeclineUserCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    public DeclineUserCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<bool>> Handle(DeclineUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = _dbContext.Accounts.Where(x => x.AccountObjectId == request.AccountInfoId).FirstOrDefault();

            if (account == null)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not exist" };

            if (account.AccountStatusId == Domain.Enums.AccountStatus.Approved || account.AccountStatusId == Domain.Enums.AccountStatus.Migrated)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account is Already Approved" };

            account.ModifiedBy = _currentUserService.UserObjId.ToString();
            account.LastModified = DateTime.UtcNow;
            account.Remarks = request.Remarks;
            account.AccountStatusId = Domain.Enums.AccountStatus.Declined;

            await _dbContext.SaveChangesAsync();
            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}
