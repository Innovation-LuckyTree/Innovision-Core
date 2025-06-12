namespace Innovision.Core.Domain.Enums;

public class AccountStatus
{
    public const int ForApproval = 1;
    public const int Approved = 2; // RUN MIGRATION HERE
    public const int Declined = 3;
    public const int Block = 4;
    public const int Migrated = 5;
    public const int Deleted = 6;
    public const int Completed = 7;
}

