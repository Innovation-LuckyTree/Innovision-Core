using AutoMapper;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Common.Enums;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Application.Requests.Deposits.Queries;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Infrastructure.AccountServices;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Deposits.Commands.AddUserDepositRequest;

public class UpdateDepositStatusCommandHandler : IRequestHandler<UpdateDepositStatusCommand, DepositDto>
{
    private readonly ICoreDbContext _coreDbContext;
    private readonly IMapper _mapper;
    private readonly IAccountServiceApi _accountServiceApi;
    private readonly IMediator _mediator;

    public UpdateDepositStatusCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, IAccountServiceApi accountServiceApi, IMediator mediator)
    {
        _coreDbContext = coreDbContext;
        _mapper = mapper;
        _accountServiceApi = accountServiceApi;
        _mediator = mediator;
    }

    public async Task<DepositDto> Handle(UpdateDepositStatusCommand request, CancellationToken cancellationToken)
    {
        var deposit = await _coreDbContext.Deposits
            .Where(o => o.DepositId == request.DepositId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = deposit ?? throw new EntityNotFoundException(typeof(Deposit).Name, request.DepositId);

        deposit.DepositStatusId = request.Status;
        deposit.Remarks = request.Remarks;

        _coreDbContext.Deposits.Update(deposit);

        await _coreDbContext.SaveChangesAsync(cancellationToken);

        var userAccount = await _coreDbContext.Accounts
                .Where(e => e.AccountInfoId == deposit.AccountInfoId)
                .FirstOrDefaultAsync(cancellationToken);

        if (request.Status == (int)DepositStatusTypes.Success)
        {
            var commObj = new AccountTransactionRequest
            {
                AccountId = userAccount.AccountObjectId,
                Amount = deposit.Amount,
                ModeOfTransaction = "WEBAPP",
                TransactionNo = deposit.TransactionNo,
                TransactionReference = $"DP{deposit.DepositId.ToString().PadLeft(16, '0')}",
                Notes = "ON-SITE, CASH-DEPOSIT",
                AccountType = WalletAccountTypes.GetWalletAccountType(userAccount.UserTypeId)
            };

            var result = await _accountServiceApi.AddWalletAccount(commObj, cancellationToken);

            await _mediator.Publish(new CreateAccountNotification(userAccount.AccountInfoId,
                    NotificationTypes.ACCOUNT_DEPOSITS, "Approved Deposit Request",
                    $"Congratulations! Your {deposit.Amount} deposit request has been approved", "/wallet"), cancellationToken);
        }

        if (request.Status == (int)DepositStatusTypes.Declined)
        {
            await _mediator.Publish(new CreateAccountNotification(userAccount.AccountInfoId,
                    NotificationTypes.ACCOUNT_DEPOSITS, "Declined Deposit Request",
                    $"Sorry, your {deposit.Amount} deposit request has been declined", "/wallet"), cancellationToken);
        }

        return _mapper.Map<DepositDto>(deposit);
    }
}