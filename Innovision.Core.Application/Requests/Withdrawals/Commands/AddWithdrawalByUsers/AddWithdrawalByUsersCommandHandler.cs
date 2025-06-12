using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Constants;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Application.Requests.Notifications.Queries.GetCredtiNotifcationById;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Innovision.Core.Application.Requests.Withdrawals.Commands.AddWithdrawalByUsers;

public class AddWithdrawalByUsersCommandHandler(ICurrentUserService currentUserService, 
    ICoreDbContext dbContext, ILogger<AddWithdrawalByUsersCommandHandler> logger,
    IMediator mediator,
    IAccountServiceApi accountServiceApi) : IRequestHandler<AddWithdrawalByUsersCommand, ApiResponse<AccountWithdrawalDto>>
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IAccountServiceApi _accountServiceApi = accountServiceApi;
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AddWithdrawalByUsersCommandHandler> _logger = logger;

    public async Task<ApiResponse<AccountWithdrawalDto>> Handle(AddWithdrawalByUsersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _dbContext.Accounts
                .Include(o => o.Branch)
                .Where(x => x.AccountInfoId == request.AccountId && x.IsActive)
                .FirstOrDefaultAsync(cancellationToken);

            _ = account ?? throw new EntityNotFoundException("Account", request);

            // if (account.UserTypeId == Domain.Enums.UserTypes.Player)
            // {
            //     var company = await _dbContext.Companies.Where(x => x.CompanyId == account.Branch.CompanyId).FirstOrDefaultAsync(cancellationToken);

            //     if (request.Amount > company?.WithdrawalLimit)
            //         throw new Exception("Withdrawal Limit");
            // }

            var withdrawal = new Withdrawal()
            {
                TransactionType = TransactionType.Withdrawal,
                AccountInfoId = account.AccountInfoId,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                Status = WalletWithdrawalStatusId.Pending,
                TransactionDate = DateTime.UtcNow
            };

            _dbContext.Withdrawals.Add(withdrawal);

            await _dbContext.SaveChangesAsync(cancellationToken);

            if (request.PaymentMethod.ToLower() != "cash")
            {
                // GetAccount wallet to get the actual balance
                var accountWallet = await _accountServiceApi.GetAccountWalletBalanceById(account.AccountObjectId, cancellationToken);
                if (accountWallet == null)
                    return new ApiResponse<AccountWithdrawalDto>() { Success = false, ErrorMessage = "Insufficient Balance!" };


                var isSufficientBalance = accountWallet.Balance >= request.Amount;

                if (!isSufficientBalance)
                {
                    return new ApiResponse<AccountWithdrawalDto>() { Success = false, ErrorMessage = "Insufficient Balance!" };
                }

                var creditTransaction = new AddCreditTransactionRequest(withdrawal.TransactionNo, withdrawal.Amount, "Withdrawal request from app");
                await _accountServiceApi.AccountWithdraw(creditTransaction, cancellationToken);
            }

            if (withdrawal.Status == WalletWithdrawalStatusId.Pending)
            {
                var companyUserIds = await _dbContext.Accounts
                .Include(m => m.Branch)
                .Where(m => ((m.UserTypeId == 2 && m.IsMain) || (m.UserType.UserTypeName.ToLower().Contains("accounting"))))
                .Select(m => new NotificationAccount { AccountId = m.AccountInfoId })
                .ToListAsync(cancellationToken);

                foreach (var item in companyUserIds)
                {
                    await _mediator.Publish(new CreateAccountNotification(item.AccountId,
                        NotificationTypes.ACCOUNT_WITHDRAWALS, "New Withdrawal Request",
                        $"{account.FirstName} {account.LastName} is requesting to withdraw {request.Amount.ToString("F2")}.", "/withdrawals"), cancellationToken);
                }
            }

            return new ApiResponse<AccountWithdrawalDto>() { Data = new AccountWithdrawalDto(withdrawal.TransactionId, withdrawal.TransactionNo) };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to Create Account Withdrawal!: {ex.Message}");

            return new ApiResponse<AccountWithdrawalDto>() { Success = false, ErrorMessage = "Failed to create withdrawal!" };
        }
    }
}
