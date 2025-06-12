using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.AccountServices.Models.Responses;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Commands.SetPaymentProvider;

public class SetPaymentProviderCommandHandler : IRequestHandler<SetPaymentProviderCommand, AccountResponse>
{
    private readonly IAccountServiceApi _accountServiceApi;
    private readonly ICoreDbContext _coreDbContext;
    private readonly ICurrentUserService _currentUserService;

    public SetPaymentProviderCommandHandler(IAccountServiceApi accountServiceApi, ICoreDbContext coreDbContext, ICurrentUserService currentUserService)
    {
        _accountServiceApi = accountServiceApi;
        _coreDbContext = coreDbContext;
        _currentUserService = currentUserService;
    }

    public async Task<AccountResponse> Handle(SetPaymentProviderCommand request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreDbContext.Accounts
            .Where(o => o.UserId == _currentUserService.UserObjId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = accountInfo ?? throw new EntityNotFoundException(typeof(Account).Name, _currentUserService.UserObjId);

        var result = await _accountServiceApi.CreatePaymentAccount(new CreateAccountRequest($"{accountInfo.FirstName} {accountInfo.LastName}", accountInfo.Email ?? "", accountInfo.MobileNumber), cancellationToken);

        if (result == null)
        {
            var accountResponse = new AccountResponse
            {
                Status = "failed",
                ErrorMessage = "Unable to create account to payment provider!"
            };

            return accountResponse;
        }

        accountInfo.PaymentAccountId = result.Data.Data.Id;

        _coreDbContext.Accounts.Update(accountInfo);

        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return result;
    }
}
