using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.CoreIdentity.Models.Requests;
using Innovision.Core.Infrastructure.CoreIdentity.Models.Responses;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Commands.AddAccountToUserIdentity;

public class AddAccountToUserIdentityCommandHandler(ICoreDbContext dbContext, IAppConfig appConfig,
    ICoreIdentityApi coreIdentityApi,
    IAccountServiceApi accountServiceApi, IMediator mediator, IMessageBrokerClientApi messageBrokerApi) : IRequestHandler<AddAccountToUserIdentityCommand, CreateUserResponse>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IAppConfig _appConfig = appConfig;
    private readonly ICoreIdentityApi _coreIdentityApi = coreIdentityApi;
    private readonly IAccountServiceApi _accountServiceApi = accountServiceApi;
    private readonly IMediator _mediator = mediator;
    private readonly IMessageBrokerClientApi _messageBrokerApi = messageBrokerApi;

    public async Task<CreateUserResponse> Handle(AddAccountToUserIdentityCommand request, CancellationToken cancellationToken)
    {
        var userStatus = Domain.Enums.AccountStatus.Completed;

        var account = await _dbContext.Accounts
            .Include(o => o.Branch)
            .Where(o => o.AccountObjectId == request.AccountInfoId)
            .SingleOrDefaultAsync(cancellationToken);

        _ = account ?? throw new EntityNotFoundException("AccountInfo", request.AccountInfoId);

        var appId = Guid.Parse(_appConfig.AppId);
        if (account.UserTypeId == Domain.Enums.UserTypes.Player)
        {
            appId = Guid.Parse(_appConfig.MobileAppId);
            userStatus = Domain.Enums.AccountStatus.Migrated;
        }

        var createUserRequest = new CreateUserRequest(account.MobileNumber ?? account.Email, account.Email ?? "", account.MobileNumber,
            request.Password ?? string.Empty, account.UserTypeId, account.IsMain, appId, "");

        var response = await _coreIdentityApi.CreateUserIdentity(createUserRequest, cancellationToken);
        if (response.IdNumber == 0) throw new Exception("Failed to create user identity!");

        account.AccountStatusId = userStatus;
        account.UserId = response.Id;
        account.LastModified = DateTime.UtcNow;
        account.LastSetPassword = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        //await NotifyAccount(account.MobileNumber, cancellationToken);
        return response;
    }

    //private async Task NotifyAccount(string mobileNumber, CancellationToken cancellationToken)
    //{
    //    var message = $"[HAPPY PLAY] Congratulations for your successful registration in Happy Play. Your Username is: {mobileNumber}, For your account's security, please login and set your password immediately.";
    //    await _mediator.Send(new SmsSendMessageCommand(mobileNumber, message, companyInfo.SmsType), cancellationToken);
    //}
}