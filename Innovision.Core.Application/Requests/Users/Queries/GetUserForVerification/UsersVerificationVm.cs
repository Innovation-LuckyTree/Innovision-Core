namespace Innovision.Core.Application.Requests.Users.Queries.GetUserForVerification
{
    public class UsersVerificationVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<UserVerificationDto> VerificationUsers { get; set; }
    }
}
