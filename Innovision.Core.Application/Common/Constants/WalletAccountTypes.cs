using Innovision.Core.Domain.Enums;

namespace Innovision.Core.Application.Common.Contants;

public class WalletAccountTypes
{
    public const string ACCOUNT_PLAYER = "ACCOUNT-PLAYER";
    public const string ACCOUNT_MASTER_AGENT = "ACCOUNT-MASTER-AGENT";
    public const string ACCOUNT_AGENT = "ACCOUNT-AGENT";

    public static string GetWalletAccountType(int userType)
    {
        return userType switch
        {
            UserTypes.MasterAgent => ACCOUNT_MASTER_AGENT,
            UserTypes.Agent => ACCOUNT_AGENT,
            UserTypes.Player => ACCOUNT_PLAYER
        };
     }
}

