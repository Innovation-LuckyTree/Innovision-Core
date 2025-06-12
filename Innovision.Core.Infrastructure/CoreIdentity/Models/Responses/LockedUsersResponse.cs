namespace Innovision.Core.Infrastructure.CoreIdentity.Models.Responses
{
    public class LockedUsersResponse
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<LockedUserDto> Results { get; set; }
    }

    public class LockedUserDto
    {
        public Guid UserId { get; set; }
        public int Attempts { get; set; }
        public  DateTimeOffset LockTime { get; set; }
    }
}
