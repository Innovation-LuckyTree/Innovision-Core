using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.AccountApproval.Commands;

public class ApproveUserVerificationCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService, IMediator mediator, IWebsocketServicesApi websocketServiceApi) : IRequestHandler<ApproveUserVerificationCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IMediator _mediator = mediator;
    private readonly IWebsocketServicesApi _websocketServiceApi = websocketServiceApi;

    public async Task<ApiResponse<bool>> Handle(ApproveUserVerificationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = _dbContext.Accounts.Where(x => x.AccountObjectId == request.AccountObjId).FirstOrDefault();

            if (account == null)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not exist" };

            account.ModifiedBy = _currentUserService.UserObjId.ToString();
            account.LastModified = DateTime.UtcNow;
            account.IsVerified = true;
            account.IsDeclined = false;
            account.ForVerification = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator
                .Publish(new CreateNotificationByNameNotification(account.AccountInfoId, NotificationTypes.MOBILE_PROFILE, NotificationNames.ApproveKYCRequest, account.MobileNumber), cancellationToken)
                .ConfigureAwait(false);

            await NotifyAccount(account.AccountInfoId, cancellationToken);
            
            await _mediator.Publish(new AccountUpdateMigrationNotification(account.AccountObjectId), cancellationToken).ConfigureAwait(false);

            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    private async Task NotifyAccount(long accountId, CancellationToken cancellationToken)
    {
      await Task.Run(async () => await _websocketServiceApi.FullyVerifiedUser(new ApproveFullyVerifiedUserRequest(accountId, true), cancellationToken), cancellationToken);
    }
}
