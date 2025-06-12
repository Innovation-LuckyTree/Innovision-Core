namespace Innovision.Core.Application.Common.Contants;


public static class UserContants
{
    public const int USER_TYPE_PLAYER = 5;
    public const int USER_TYPE_MASTER_AGENT = 3;
    public const int USER_TYPE_AGENT = 4;

    public static IEnumerable<int> ACCOUNT_TYPES_WITH_WALLET = [
        USER_TYPE_PLAYER, USER_TYPE_MASTER_AGENT, USER_TYPE_AGENT
    ];
}