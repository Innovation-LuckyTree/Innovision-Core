using AutoMapper;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Common.Enums;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Application.Requests.Deposits.Queries;
using Innovision.Core.Application.Requests.Notifications.Queries.GetCredtiNotifcationById;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Deposits.Commands.AddUserDepositRequest;

public class AddUserDepositRequestCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, ICurrentUserService currentUser, IAccountServiceApi accountServiceApi, IMediator mediator) : IRequestHandler<AddUserDepositRequestCommand, DepositDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ICurrentUserService _currentUser = currentUser;
    private readonly IAccountServiceApi _accountServiceApi = accountServiceApi;
    private readonly IMediator _mediator = mediator;

    public async Task<DepositDto> Handle(AddUserDepositRequestCommand request, CancellationToken cancellationToken)
    {
        var userAccount = await _coreDbContext.Accounts
            .Include(m => m.Branch)
            .Where(e => e.AccountInfoId == request.AccountInfoId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = userAccount ?? throw new EntityNotFoundException(typeof(Account).Name, request.AccountInfoId);

        Deposit deposit = new()
        {
            AccountInfoId = userAccount.AccountInfoId,
            Amount = request.Amount,
            DepositStatusId = request.Status,
            PaymentMethodId = request.PaymentMethod,
            TransactionDate = DateTime.UtcNow,
            TransactionType = request.TransactionType
        };

        _coreDbContext.Deposits.Add(deposit);
        await _coreDbContext.SaveChangesAsync(cancellationToken);

        if (request.Status == (int)DepositStatusTypes.Success)
        {
            // add wallet account
            var commObj = new AccountTransactionRequest
            {
                AccountId = userAccount.AccountObjectId,
                Amount = request.Amount,
                ModeOfTransaction = "WEBAPP",
                TransactionNo = deposit.TransactionNo,
                TransactionReference = $"DP{deposit.DepositId.ToString().PadLeft(16, '0')}",
                Notes = "ON-SITE, CASH-DEPOSIT",
                AccountType = WalletAccountTypes.GetWalletAccountType(userAccount.UserTypeId)
            };

            var result = await _accountServiceApi.AddWalletAccount(commObj, cancellationToken);

            await _mediator.Publish(new CreateAccountNotification(userAccount.AccountInfoId,
                    NotificationTypes.ACCOUNT_DEPOSITS, "Approved Deposit Request",
                    $"Congratulations! Your {request.Amount} deposit request has been approved", "/wallet"), cancellationToken);
        }

        if (request.Status == (int)DepositStatusTypes.Declined)
        {
            await _mediator.Publish(new CreateAccountNotification(userAccount.AccountInfoId,
                    NotificationTypes.ACCOUNT_DEPOSITS, "Declined Deposit Request",
                    $"Sorry, your {request.Amount} deposit request has been declined", "/wallet"), cancellationToken);
        }

        if (request.Status == (int)DepositStatusTypes.Pending)
        {
            var companyUserIds = await _coreDbContext.Accounts
            .Include(m => m.Branch)
            .Where(m => ((m.UserTypeId == 2 && m.IsMain) || (m.UserType.UserTypeName.ToLower().Contains("accounting"))))
            .Select(m => new NotificationAccount { AccountId = m.AccountInfoId })
            .ToListAsync(cancellationToken);

            foreach (var item in companyUserIds)
            {
                await _mediator.Publish(new CreateAccountNotification(item.AccountId,
                    NotificationTypes.ACCOUNT_DEPOSITS, "Pending Deposit Request",
                    $"{userAccount.FirstName} {userAccount.LastName} is requesting to deposit {request.Amount}.", "/wallet"), cancellationToken);
            }
        }

        return _mapper.Map<DepositDto>(deposit);
    }
}