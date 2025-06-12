namespace Innovision.Core.Application.Requests.Users.Queries
{
    public class UserStatusVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string CurrentDrawTime { get; set; }

        public List<UserStatusDto> Results { get; set; }
    }
}
