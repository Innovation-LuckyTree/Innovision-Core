using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Users.AccountApproval.Commands;

public class ApprovedUserCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService, IAccountServices accountServices, IMediator mediator) : IRequestHandler<ApprovedUserCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IAccountServices _accountServices = accountServices;
    private readonly IMediator _mediator = mediator;

    public async Task<ApiResponse<bool>> Handle(ApprovedUserCommand request, CancellationToken cancellationToken)
    {
        var account = _dbContext.Accounts.Where(x => x.AccountObjectId == request.AccountInfoId).FirstOrDefault();

        if (account == null)
            return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not exist" };

        if (account.AccountStatusId == Domain.Enums.AccountStatus.Approved || account.AccountStatusId == Domain.Enums.AccountStatus.Migrated)
            return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account is Already Approved" };

        //var refferralAccount = _dbContext.Accounts.Where(x => x.RefferralKey == account.RefferralCode).FirstOrDefault();
        account.ModifiedBy = _currentUserService.UserObjId.ToString();
        account.LastModified = DateTime.UtcNow;
        account.IsActive = true;
        account.AccountStatusId = Domain.Enums.AccountStatus.Approved;
        account.AccountHistories = [new AccountHistory { Action = "APPROVE", CreatedOn = DateTime.UtcNow }];

        if (request.UserTypeId.HasValue)
            account.UserTypeId = request.UserTypeId.Value;
        else
        {
            account.Commision = request.Commission.Value;
            account.UserTypeId = Domain.Enums.UserTypes.Agent;
            account.RefferralKey = _accountServices.GenerateCode(8);
        }

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);

            //await NotifyAccount(account.MobileNumber, cancellationToken);
            await _mediator.Publish(new AddAccountMigrationNotification(account.AccountObjectId), cancellationToken).ConfigureAwait(false);

            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    //private async Task NotifyAccount(string mobileNumber, CancellationToken cancellationToken)
    //{
    //    var companyInfo = await _mediator.Send(new GetDefaultCompanyQuery(), cancellationToken);

    //    var message = $"[HAPPY PLAY] Congratulations for your successful registration in Happy Play. Your Username is: {mobileNumber}, For your account's security, please login and set your password immediately.";
    //    await _mediator.Send(new SmsSendMessageCommand(mobileNumber, message, companyInfo.SmsType), cancellationToken);
    //}
}