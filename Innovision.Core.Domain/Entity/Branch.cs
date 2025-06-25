using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class Branch : AuditableEntity
{
    public int BranchId { get; set; }
    public Guid? BranchCreditObjectId { get; set; }
    public Guid? BranchBonusObjectId { get; set; }
    public string BranchName { get; set; }
    public string BranchCode { get; set; }
    public bool IsMain { get; set; }
    public bool IsActive { get; set; }

    public long? GameSiteManagerId { get; set; }
    public long? GameSiteAccountId { get; set; }
    public long? DefaultAccountId { get; set; }
    public long? AddressId { get; set; }

    public virtual Address Address { get; set; }
    public virtual ICollection<Account> Account { get; set; } = new HashSet<Account>();
    public virtual ICollection<Announcement> Announcements { get; set; }
    public virtual ICollection<LiveStream> LiveStreams { get; set; }
}