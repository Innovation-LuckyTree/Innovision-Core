using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity
{
    public class AccountHistory : AuditableEntity
    {
        public long AccountHistoryId { get; set; }
        public long AccountInfoId { get; set; }
        public string Action { get; set; }
        public virtual Account Account { get; set; }
    }
}
