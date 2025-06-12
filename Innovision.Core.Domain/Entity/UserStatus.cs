namespace Innovision.Core.Domain.Entity
{
    public class UserStatus
    {
        public long UserStatusId { get; set; }
        public long AccountInfoId { get; set; }
        public int? Status { get; set; }
        public int? SubStatus { get; set; }

        public virtual Account Account { get; set; }
    }
}
