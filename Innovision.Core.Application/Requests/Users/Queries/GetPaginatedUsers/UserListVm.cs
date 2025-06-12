namespace Innovision.Core.Application.Requests.Users.Queries.GetPaginatedUsers
{
    public class UserListVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<SystemUserDto> UserList { get; set; }
    }
}