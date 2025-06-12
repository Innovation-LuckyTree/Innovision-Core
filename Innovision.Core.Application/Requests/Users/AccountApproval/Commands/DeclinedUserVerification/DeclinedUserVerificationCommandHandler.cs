using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.AccountApproval.Commands;

public class DeclinedUserVerificationCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService, IMediator mediator, IWebsocketServicesApi websocketServiceApi) : IRequestHandler<DeclinedUserVerificationCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IMediator _mediator = mediator;
    private readonly IWebsocketServicesApi _websocketServiceApi = websocketServiceApi;

    public async Task<ApiResponse<bool>> Handle(DeclinedUserVerificationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = _dbContext.Accounts.Where(x => x.AccountObjectId == request.AccountObjectId).FirstOrDefault();

            if (account == null)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not exist" };

            account.ModifiedBy = _currentUserService.UserObjId.ToString();
            account.LastModified = DateTime.UtcNow;
            account.Remarks = request.Remarks;
            account.IsVerified = false;
            account.IsDeclined = true;
            account.ForVerification = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator
                .Publish(new CreateNotificationByNameNotification(account.AccountInfoId, NotificationTypes.MOBILE_PROFILE, NotificationNames.DeclineKYCRequest, account.MobileNumber)
                {
                    Params = [request.Remarks]
                }, cancellationToken)
                .ConfigureAwait(false);

            await NotifyAccount(account.AccountInfoId, cancellationToken);

            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    private async Task NotifyAccount(long accountId, CancellationToken cancellationToken)
    {
        await Task.Run(async () => await _websocketServiceApi.FullyVerifiedUser(new ApproveFullyVerifiedUserRequest(accountId, false), cancellationToken), cancellationToken);
    }
}
