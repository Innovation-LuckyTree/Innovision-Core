using AutoMapper;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Constants;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Application.Requests.Notifications.Queries.GetCredtiNotifcationById;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using HappyPlay.Upload.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Withdrawals.Commands.AddWithdrawalByAccounting;

public class AddWithdrawalByAccountingCommandHandler : IRequestHandler<AddWithdrawalByAccountingCommand, ApiResponse<WithdrawalDto>>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;
    private readonly IAccountServiceApi _accountServiceApi;
    private readonly IMediator _mediator;

    public AddWithdrawalByAccountingCommandHandler(ICoreDbContext dbContext, IAccountServiceApi accountServiceApi, IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _accountServiceApi = accountServiceApi;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ApiResponse<WithdrawalDto>> Handle(AddWithdrawalByAccountingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _dbContext.Accounts
                .Include(o => o.Branch)
                .Where(x => x.AccountInfoId == request.AccountInfoId && x.IsActive)
                .FirstOrDefaultAsync(cancellationToken);

            if (account == null)
                return new ApiResponse<WithdrawalDto>() { Success = false, ErrorMessage = $"Unable to find account with AccountId {request.AccountInfoId}!" };


            // check status
            if (request.Status == WalletWithdrawalStatusId.Complete)
            {
                var accountWallet = await _accountServiceApi.GetAccountWalletBalanceById(account.AccountObjectId, cancellationToken);

                if (accountWallet == null)
                    return new ApiResponse<WithdrawalDto>() { Success = false, ErrorMessage = "Insufficient Balance!" };

                var isSufficientBalance = accountWallet.Balance >= request.Amount;

                if (!isSufficientBalance)
                    return new ApiResponse<WithdrawalDto>() { Success = false, ErrorMessage = "Insufficient Balance!" };
            }

            Crypto crypto = new();
            var withdrawal = new Withdrawal
            {
                TransactionType = TransactionType.Withdrawal,
                AccountInfoId = account.AccountInfoId,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                Status = request.Status,
                BankInfo = (request.BankInfo != null) ? crypto.Encrypt($"{request.BankInfo.AccountName}|{request.BankInfo.AccountNumber}") : null,
                BankReferenceId = (request.BankInfo != null) ? request.BankInfo.BankReferenceId : null
            };

            _dbContext.Withdrawals.Add(withdrawal);
            await _dbContext.SaveChangesAsync();

            // check status
            if (request.Status == WalletWithdrawalStatusId.Complete)
            {
                var result = await _accountServiceApi.WithdrawBalance(new WithdrawBalanceRequest
                {
                    AccountId = account.AccountObjectId,
                    Amount = request.Amount,
                    ModeOfTransaction = "CASH",
                    Notes = "CASH-WITHDRAWAL",
                    TransactionNo = withdrawal.TransactionNo
                }, cancellationToken);

                var notifTitle = "Successful Withdrawal Request";
                var notifDesc = $"Your {request.Amount.ToString("F2")} withdrawal request has been approved.";
                await _mediator.Publish(new CreateAccountNotification(account.AccountInfoId,
                    NotificationTypes.ACCOUNT_WITHDRAWALS, notifTitle, notifDesc, "/withdrawals"), cancellationToken);
            }

            if (request.Status == WalletWithdrawalStatusId.Pending)
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

            return new ApiResponse<WithdrawalDto>() { Data = _mapper.Map<WithdrawalDto>(withdrawal) };
        }
        catch (Exception ex)
        {
            return new ApiResponse<WithdrawalDto>() { Success = false, ErrorMessage = ex.Message };
        }

    }
}
