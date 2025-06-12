namespace Innovision.Core.Application.Common.Constants;

public class WalletWithdrawalStatusString
{
    public const string Pending = "Pending";
    public const string Complete = "Complete";
    public const string Failed = "Failed";
    public const string Declined = "Declined";
    public const string Void = "Void";
}
public class WalletWithdrawalStatusId
{
    public const int Pending = 0;
    public const int Complete = 1;
    public const int Declined = 2;
    public const int Void = 3;
    public const int Failed = 4;
}
public class WalletWithdrawal
{
    public const string InsuffienceBalance = "Insuffience Balance";
}

public class TransactionType
{
    public const string Withdrawal = "WD";
}