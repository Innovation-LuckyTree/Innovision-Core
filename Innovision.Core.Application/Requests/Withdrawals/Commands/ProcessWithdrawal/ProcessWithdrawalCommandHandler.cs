using AutoMapper;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Constants;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Withdrawals.Commands.ProcessWithdrawal;

public class ProcessWithdrawalCommandHandler : IRequestHandler<ProcessWithdrawalCommand, ApiResponse<WithdrawalDto>>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _coreDbcontext;
    private readonly IAccountServiceApi _accountServiceApi;
    private readonly IMediator _mediator;

    public ProcessWithdrawalCommandHandler(ICoreDbContext coreDbcontext, IAccountServiceApi accountServiceApi, IMapper mapper, IMediator mediator)
    {
        _coreDbcontext = coreDbcontext;
        _accountServiceApi = accountServiceApi;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ApiResponse<WithdrawalDto>> Handle(ProcessWithdrawalCommand request, CancellationToken cancellationToken)
    {
        var withdrawalTransaction = await _coreDbcontext.Withdrawals
            .Include(m => m.AccountInfo)
            .Where(o => o.TransactionId == request.TransactionId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = withdrawalTransaction ?? throw new EntityNotFoundException(typeof(Withdrawal).Name, request.TransactionId);

        // now check balance
        // GetAccount wallet to get the actual balance
        var accountWallet = await _accountServiceApi.GetAccountWalletBalanceById(withdrawalTransaction.AccountInfo.AccountObjectId, cancellationToken);

        if (accountWallet == null)
            return new ApiResponse<WithdrawalDto>() { Success = false, ErrorMessage = "Insufficient Balance!" };

        var isSufficientBalance = accountWallet.Balance >= withdrawalTransaction.Amount;

        if (!isSufficientBalance)
            return new ApiResponse<WithdrawalDto>() { Success = false, ErrorMessage = "Insufficient Balance!" };

        withdrawalTransaction.Status = request.Status;
        withdrawalTransaction.LastModified = DateTime.UtcNow;
        await _coreDbcontext.SaveChangesAsync(cancellationToken);

        if (request.Status == WalletWithdrawalStatusId.Complete)
        {
            await _accountServiceApi.WithdrawBalance(new WithdrawBalanceRequest
            {
                AccountId = withdrawalTransaction.AccountInfo.AccountObjectId,
                Amount = withdrawalTransaction.Amount,
                ModeOfTransaction = "CASH",
                Notes = "CASH-WITHDRAWAL",
                TransactionNo = withdrawalTransaction.TransactionNo
            }, cancellationToken);

            // notify
            await _mediator.Publish(new CreateAccountNotification(withdrawalTransaction.AccountInfoId,
                    NotificationTypes.ACCOUNT_WITHDRAWALS, "Successful Withdrawal Request",
                    $"Your {withdrawalTransaction.Amount.ToString("F2")} withdrawal request has been approved.", "/withdrawals"), cancellationToken);
        }

        if (request.Status == WalletWithdrawalStatusId.Declined)
            await _mediator.Publish(new CreateAccountNotification(withdrawalTransaction.AccountInfoId,
                NotificationTypes.ACCOUNT_WITHDRAWALS, "Declined Withdrawal Request",
                $"Sorry, your {withdrawalTransaction.Amount.ToString("F2")} withdrawal request has been declined with the following remarks: \n Unable to process requst.", "/withdrawals"), cancellationToken);

        return new ApiResponse<WithdrawalDto>() { Data = _mapper.Map<WithdrawalDto>(withdrawalTransaction) };
    }
}