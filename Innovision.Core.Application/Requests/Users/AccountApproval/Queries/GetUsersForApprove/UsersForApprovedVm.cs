namespace Innovision.Core.Application.Requests.Users.AccountApproval.Queries.GetUsersForApprove
{
    public class UsersForApprovedVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<UsersForApprovedDto> Results { get; set; }
    }
}
