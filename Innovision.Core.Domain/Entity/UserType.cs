namespace Innovision.Core.Domain.Entity;

public partial class UserType
{
    public int UserTypeId { get; set; }
    public string UserTypeName { get; set; }
    public int GroupType { get; set; } // 0 - Dashboard, 1 - Accounting, 2 - Support
    public int RoleType { get; set; } // 0 - admin , 1 - company, 2 - branch
    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Account> Accounts { get; set; }
    public virtual ICollection<UserTypeConfig> UserTypeAccessControls { get; set; }
}
