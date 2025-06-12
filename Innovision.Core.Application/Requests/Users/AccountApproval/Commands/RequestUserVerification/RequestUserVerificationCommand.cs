using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.AccountApproval.Commands.RequestUserVerification
{
    public record RequestUserVerificationCommand(Guid AccountObjectId) : IRequest<ApiResponse<bool>>;

    public class RequestUserVerificationCommandHandler : IRequestHandler<RequestUserVerificationCommand, ApiResponse<bool>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        public RequestUserVerificationCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<bool>> Handle(RequestUserVerificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = _dbContext.Accounts.Where(x => x.AccountObjectId == request.AccountObjectId).FirstOrDefault();

                if (account == null)
                    return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not exist" };

                account.ModifiedBy = _currentUserService.UserObjId.ToString();
                account.LastModified = DateTime.UtcNow;
                account.IsVerified = false;
                account.IsDeclined = false;
                account.ForVerification = true;

                await _dbContext.SaveChangesAsync();
                return new ApiResponse<bool>() { Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
