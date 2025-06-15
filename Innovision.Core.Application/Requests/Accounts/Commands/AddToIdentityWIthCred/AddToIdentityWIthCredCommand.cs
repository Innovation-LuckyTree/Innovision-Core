using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.CoreIdentity.Models.Requests;
using Innovision.Core.Infrastructure.CoreIdentity.Models.Responses;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Commands.AddToUserIdentityWIthCredential
{
    public record AddToIdentityWIthCredCommand(Guid AccountInfoId, string Password) : IRequest<CreateUserResponse>;

    public class AddToIdentityWIthCredCommandHandler(ICoreDbContext dbContext, IAppConfig appConfig,
    ICoreIdentityApi coreIdentityApi, IAccountServiceApi accountServiceApi, IMediator mediator) : IRequestHandler<AddToIdentityWIthCredCommand, CreateUserResponse>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IAppConfig _appConfig = appConfig;
        private readonly ICoreIdentityApi _coreIdentityApi = coreIdentityApi;
        private readonly IAccountServiceApi _accountServiceApi = accountServiceApi;
        private readonly IMediator _mediator = mediator;

        public async Task<CreateUserResponse> Handle(AddToIdentityWIthCredCommand request, CancellationToken cancellationToken)
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
                //userStatus = Domain.Enums.AccountStatus.Migrated;
            }

            var createUserRequest = new CreateUserRequest(account.UserName ?? account.Email, account.Email ?? "", account.MobileNumber,
                request.Password, account.UserTypeId, account.IsMain, appId, Guid.NewGuid().ToString());

            var response = await _coreIdentityApi.CreateUserIdentity(createUserRequest, cancellationToken);
            if (response.IdNumber == 0) throw new Exception("Failed to create user identity!");

            var paymentProviderResponse = await _accountServiceApi.CreatePaymentAccount(new CreateAccountRequest($"{account.FirstName} {account.LastName}", account.Email ?? "", account.MobileNumber), cancellationToken);

            if (paymentProviderResponse?.Data?.Data != null)
            {
                account.PaymentAccountId = paymentProviderResponse.Data.Data.Id;
            }

            account.AccountStatusId = Domain.Enums.AccountStatus.Completed;
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
}
