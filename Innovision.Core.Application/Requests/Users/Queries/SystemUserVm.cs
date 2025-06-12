namespace Innovision.Core.Application.Requests.Users.Queries
{
    public class SystemUserVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<SystemUserDto> Results { get; set; }
    }
}
