namespace Innovision.Core.Application.Common.Contants;


public static class RegisteredAccountStatus
{
    public static IEnumerable<int> EXISTING_ACCOUNT_STATUS = [
        Domain.Enums.AccountStatus.ForApproval,
        Domain.Enums.AccountStatus.Migrated,
        Domain.Enums.AccountStatus.Approved,
        Domain.Enums.AccountStatus.Block,
        Domain.Enums.AccountStatus.Completed
    ];
}


