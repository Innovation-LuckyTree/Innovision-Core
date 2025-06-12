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

namespace Innovision.Core.Application.Requests.Withdrawals.Commands.AddWithdrawalByUsers;

public class UpdateWithdrawalStatusCommandHandler(ICoreDbContext coreDbcontext, IAccountServiceApi accountServiceApi, IMapper mapper, IMediator mediator) : IRequestHandler<UpdateWithdrawalStatusCommand, ApiResponse<WithdrawalDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ICoreDbContext _coreDbcontext = coreDbcontext;
    private readonly IAccountServiceApi _accountServiceApi = accountServiceApi;
    private readonly IMediator _mediator = mediator;

    public async Task<ApiResponse<WithdrawalDto>> Handle(UpdateWithdrawalStatusCommand request, CancellationToken cancellationToken)
    {
        var withdrawalTransaction = await _coreDbcontext.Withdrawals
            .Include(m => m.AccountInfo)
            .Where(o => o.TransactionId == request.TransactionId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = withdrawalTransaction ?? throw new EntityNotFoundException(typeof(Withdrawal).Name, request.TransactionId);

        if (request.Status == WalletWithdrawalStatusId.Complete)
        {
            if (!withdrawalTransaction.PaymentMethod.Equals("gcash", StringComparison.CurrentCultureIgnoreCase))
            {
                var accountWallet = await _accountServiceApi.GetAccountWalletBalanceById(withdrawalTransaction.AccountInfo.AccountObjectId, cancellationToken);

                if (accountWallet == null || (accountWallet.Balance <= withdrawalTransaction.Amount && withdrawalTransaction.PaymentMethod.ToLower() != "gcash"))
                    return new ApiResponse<WithdrawalDto>() { Success = false, ErrorMessage = "Insufficient Balance!" };

                await ProcessWithdrawBalance(withdrawalTransaction, cancellationToken);
            }
        }

        withdrawalTransaction.Status = request.Status;
        withdrawalTransaction.LastModified = DateTime.UtcNow;
        withdrawalTransaction.ImageProof = request.ImageProof;
        if (!string.IsNullOrEmpty(request.Remarks))
            withdrawalTransaction.Remarks = request.Remarks;

        // for decline notification
        if (request.Status == WalletWithdrawalStatusId.Declined)
            await _mediator.Publish(new CreateAccountNotification(withdrawalTransaction.AccountInfoId,
                NotificationTypes.ACCOUNT_WITHDRAWALS, "Declined Withdrawal Request",
                $"Sorry, your {withdrawalTransaction.Amount:F2} withdrawal request has been declined with the following remarks: \n {withdrawalTransaction.Remarks}.", "/withdrawals"), cancellationToken);

        await _coreDbcontext.SaveChangesAsync(cancellationToken);

        return new ApiResponse<WithdrawalDto>() { Data = _mapper.Map<WithdrawalDto>(withdrawalTransaction) };
    }

    private async Task ProcessWithdrawBalance(Withdrawal withdrawalTransaction, CancellationToken cancellationToken)
    {
        await _accountServiceApi.WithdrawBalance(new WithdrawBalanceRequest
        {
            AccountId = withdrawalTransaction.AccountInfo.AccountObjectId,
            Amount = withdrawalTransaction.Amount,
            ModeOfTransaction = withdrawalTransaction.PaymentMethod,
            Notes = withdrawalTransaction.PaymentMethod.Equals("cash", StringComparison.CurrentCultureIgnoreCase) ? "CASH-WITHDRAWAL" : "BANK-WITHDRAWAL",
            TransactionNo = withdrawalTransaction.TransactionNo
        }, cancellationToken);

        // now check the result before proceeding
        await _mediator.Publish(new CreateAccountNotification(withdrawalTransaction.AccountInfoId,
            NotificationTypes.ACCOUNT_WITHDRAWALS, "Successful Withdrawal Request",
            $"Your {withdrawalTransaction.Amount:F2} withdrawal request has been approved.", "/withdrawals"), cancellationToken);
    }
}