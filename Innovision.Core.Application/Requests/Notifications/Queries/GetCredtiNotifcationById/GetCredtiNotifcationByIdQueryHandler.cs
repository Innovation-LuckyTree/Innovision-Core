using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Enums;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Notifications.Queries.GetCredtiNotifcationById;

public class GetCredtiNotifcationByIdQueryHandler(ICoreDbContext dbContext, IAppConfig appConfig, IPaymentServicesApi paymentServicesApi) : IRequestHandler<GetCredtiNotifcationByIdQuery, ApiResponse<NotificationRecipient>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IAppConfig _appConfig = appConfig;
    private readonly IPaymentServicesApi _paymentServicesApi = paymentServicesApi;

    public async Task<ApiResponse<NotificationRecipient>> Handle(GetCredtiNotifcationByIdQuery request, CancellationToken cancellationToken)
    {
        var creditTrans = await _paymentServicesApi.GetCreditTransaction(request.CreditTransId, cancellationToken);
        if (creditTrans == null)
            return new ApiResponse<NotificationRecipient>() { Data = new NotificationRecipient() };

        var notifRecipient = new NotificationRecipient();
        var notifInfo = new NotificationInfo();

        // Send Credit, notify only the receiver
        if (creditTrans.TransType == (int)CreditTransactionTypes.SendCredit && creditTrans.Status == (int)CreditStatuses.Credited)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.ReceiverCreditId, cancellationToken); ;
            notifInfo.Type = "LoadingStation";
            notifInfo.NotificationName = "DirectSendCredit";
            notifInfo.Args = new List<string>() { creditTrans.Amount.ToString("F2"), creditTrans.SenderName };

            notifRecipient.RecieverNotification = notifInfo;
        }

        // Request Credit, notify only the Receiver
        if (creditTrans.TransType == (int)CreditTransactionTypes.RequestCredit && creditTrans.Status == (int)CreditStatuses.Pending)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.ReceiverCreditId, cancellationToken);
            notifInfo.Type = "LoadingStation";
            notifInfo.NotificationName = "NewCreditRequestsFromUsers";
            notifInfo.Args = new List<string>() { creditTrans.SenderName, creditTrans.Amount.ToString("F2") };

            notifRecipient.RecieverNotification = notifInfo;
        }

        // Approve Request Credit, notify only the Sender
        if (creditTrans.TransType == (int)CreditTransactionTypes.RequestCredit && creditTrans.Status == (int)CreditStatuses.Credited)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.SenderCreditId, cancellationToken);
            notifInfo.Type = "LoadingStation";
            notifInfo.NotificationName = "ApproveCreditRequest";
            notifInfo.Args = new List<string>() { creditTrans.Amount.ToString("F2") };

            notifRecipient.SenderNotification = notifInfo;
        }

        // Declined Request Credit, notify only the sender
        if (creditTrans.TransType == (int)CreditTransactionTypes.RequestCredit && creditTrans.Status == (int)CreditStatuses.Declined)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.SenderCreditId, cancellationToken);
            notifInfo.Type = "LoadingStation";
            notifInfo.NotificationName = "DeclineCreditRequest";
            notifInfo.Args = new List<string>() { creditTrans.Notes };

            notifRecipient.SenderNotification = notifInfo;
        }

        // Transfer Credit, notify only the sender
        if (creditTrans.TransType == (int)CreditTransactionTypes.TransferCredit && creditTrans.Status == (int)CreditStatuses.Credited)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.SenderCreditId, cancellationToken);
            notifInfo.Type = "LoadingStation";
            notifInfo.NotificationName = "DirectTransferCredit";
            notifInfo.Args = new List<string>() { creditTrans.Amount.ToString("F2") };

            notifRecipient.SenderNotification = notifInfo;
        }

        // Request Withdrawal Credit, notify only the receiver
        if (creditTrans.TransType == (int)CreditTransactionTypes.RequestWithdrawal && creditTrans.Status == (int)CreditStatuses.Pending)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.ReceiverCreditId, cancellationToken);
            notifInfo.Type = "Withdrawals";
            notifInfo.NotificationName = "NewWithdrawalRequestsFromUsers";
            notifInfo.Args = new List<string>() { creditTrans.SenderName, creditTrans.Amount.ToString("F2") };

            notifRecipient.RecieverNotification = notifInfo;
        }

        // Request Withdrawal Decline, notify only the Sender
        if (creditTrans.TransType == (int)CreditTransactionTypes.RequestWithdrawal && creditTrans.Status == (int)CreditStatuses.Declined)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.SenderCreditId, cancellationToken);
            notifInfo.Type = "Wallet";
            notifInfo.NotificationName = "DeclineWithdrawalRequest";
            notifInfo.Args = new List<string>() { creditTrans.Amount.ToString("F2"), creditTrans.Notes };

            notifRecipient.RecieverNotification = notifInfo;
        }

        // Approve Withdrawal Credit, notify only the Sender
        if (creditTrans.TransType == (int)CreditTransactionTypes.RequestWithdrawal && creditTrans.Status == (int)CreditStatuses.Credited)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.SenderCreditId, cancellationToken);
            notifInfo.Type = "LoadingStation";
            notifInfo.NotificationName = "ApproveWithdrawalCreditRequest";
            notifInfo.Args = new List<string>() { creditTrans.Amount.ToString("F2"), creditTrans.ReceiverName };

            notifRecipient.SenderNotification = notifInfo;
        }

        // Request Bonus Credit, notify only the receiver
        if (creditTrans.TransType == (int)CreditTransactionTypes.RequestBonus && creditTrans.Status == (int)CreditStatuses.Pending)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.ReceiverCreditId, cancellationToken);
            notifInfo.Type = "LoadingStation";
            notifInfo.NotificationName = "NewBonusRequestsFromUsers";
            notifInfo.Args = new List<string>() { creditTrans.SenderName, creditTrans.Amount.ToString("F2") };

            notifRecipient.RecieverNotification = notifInfo;
        }

        // Approve Bonus Credit, notify only the Sender
        if (creditTrans.TransType == (int)CreditTransactionTypes.RequestBonus && creditTrans.Status == (int)CreditStatuses.Credited)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.SenderCreditId, cancellationToken);
            notifInfo.Type = "LoadingStation";
            notifInfo.NotificationName = "ApproveBonusCreditRequest";
            notifInfo.Args = new List<string>() { creditTrans.Amount.ToString("F2") };

            notifRecipient.SenderNotification = notifInfo;
        }

        // Successful Withdrawal in site
        if (creditTrans.TransType == (int)CreditTransactionTypes.OnSiteWithdrawal && creditTrans.Status == (int)CreditStatuses.Credited)
        {
            notifInfo.Accounts = await GetAccounts(creditTrans.SenderCreditId, cancellationToken);
            notifInfo.Type = "Wallet";
            notifInfo.NotificationName = "SuccessfulWithdrawal";
            notifInfo.Args = new List<string>() { creditTrans.Amount.ToString("F2") };

            notifRecipient.RecieverNotification = notifInfo;
        }

        return new ApiResponse<NotificationRecipient>() { Data = notifRecipient };
    }

    private async Task<List<NotificationAccount>> GetAccounts(Guid objectId, CancellationToken cancellationToken)
    {
        var accounts = await _dbContext.Accounts.Where(m => m.AccountObjectId == objectId)
            .Select(m => new NotificationAccount
            {
                AccountId = m.AccountInfoId,
                Name = $"{m.FirstName} {m.LastName}",
                UserTypeId = m.UserTypeId,
                UserId = m.UserId
            })
            .ToListAsync(cancellationToken);

        if (accounts.Any())
            return accounts;

        // try to get branch credit object id
        var accntByBranch = await _dbContext.Accounts
            .Include(m => m.Branch)
            .Include(m => m.UserType)
            .Where(m => m.Branch.BranchCreditObjectId == objectId
                && (m.UserType.UserTypeName.ToLower().Contains("cashier")
                || (m.UserTypeId == 2 && m.IsMain)))
            .Select(m => new NotificationAccount
            {
                AccountId = m.AccountInfoId,
                Name = $"{m.FirstName} {m.LastName}",
                UserTypeId = m.UserTypeId,
                UserId = m.UserId
            })
            .ToListAsync(cancellationToken);

        if (accntByBranch.Any())
            return accntByBranch;

        // try by company credit object id
        var accntByCompany = await _dbContext.Accounts
            .Include(m => m.Branch)
            .Where(m => ((m.UserTypeId == 2 && m.IsMain) || (m.UserType.UserTypeName.ToLower().Contains("accounting"))))
            .Select(m => new NotificationAccount
            {
                AccountId = m.AccountInfoId,
                Name = $"{m.FirstName} {m.LastName}",
                UserTypeId = m.UserTypeId,
                UserId = m.UserId
            })
            .ToListAsync(cancellationToken);

        if (accntByCompany.Any())
            return accntByCompany;

        // get default service provider
        var serviceProviders = await _dbContext.Accounts
            .Include(m => m.UserType)
            .Where(m => m.UserType.UserTypeName.ToLower().Contains("service provider"))
            .Select(m => new NotificationAccount
            {
                AccountId = m.AccountInfoId,
                Name = $"{m.FirstName} {m.LastName}",
                UserTypeId = m.UserTypeId,
                UserId = m.UserId
            })
            .ToListAsync(cancellationToken);

        return serviceProviders;
    }
}
