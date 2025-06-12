using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity
{
    public partial class PlayerActivity : AuditableEntity
    {
        public long ActivityId { get; set; }
        public long AccountInfoId { get; set; }
        public int MissedDraws { get; set; } = 0;
        public int Extended { get; set; } = 0;
        public bool RequiredTopay { get; set; }
        public  DateTimeOffset? ExcludeDateTime { get; set; }
        public  DateTimeOffset? LastDrawDateTime { get; set; }
        public TimeSpan? LastDrawTime { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Account Account { get; set; }
    }
}
