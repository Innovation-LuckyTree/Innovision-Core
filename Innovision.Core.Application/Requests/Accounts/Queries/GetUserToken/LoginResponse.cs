namespace Innovision.Core.Application.Requests.Accounts.Queries.GetUserToken
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public Guid AccountObjectId { get; set; }
        public int IdNumber { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string ClientId { get; set; }
        public long ExpirationDate { get; set; }
        public bool Status { get; set; } = true;
        public int GroupType { get; set; } = 0; // 0 - Dashboard, 1 - Accounting, 2 - Support
        public int RoleType { get; set; } = 0; // 0 - admin , 1 - company, 2 - branch
        public string Fullname { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public long AccountInfoId { get; set; }
        public Guid AccountCreditId { get; set; }
        public int UserTypeId { get; set; }
        public bool IsMain { get; set; }
        public Guid BranchCreditObjectId { get; set; }
        public string RefreshToken { get; set; }
        public Guid AccountBonusId { get; set; }
        public string UserTypeName { get; set; }
        public int? FmTypeId { get; set; }
        public string ReferralCode { get; set; }
        public Guid BranchBonusObjectId { get; set; }
    }
}
