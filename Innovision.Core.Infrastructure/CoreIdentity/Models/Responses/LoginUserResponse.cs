namespace Innovision.Core.Infrastructure.CoreIdentity.Models.Responses;

public class LoginUserResponse
{
    public Guid Id { get; set; }
    public Guid AccountObjectId { get; set; }
    public int IdNumber { get; set; }
    public string UserName { get; set; }
    public string Token { get; set; }
    public string ClientId { get; set; }
    public string Type { get; set; }
    public long ExpirationDate { get; set; }
    public bool Status { get; set; } = true;
    public List<string> MenuList { get; set; }
    public string RefferralKey { get; set; }
    public Guid CompanyObjId { get; set; }
    public string CompanyName { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public string Name { get; set; }
    public string MobileNumber { get; set; }
    public bool IsMain { get; set; }
    public string RefreshToken { get; set; }
}
