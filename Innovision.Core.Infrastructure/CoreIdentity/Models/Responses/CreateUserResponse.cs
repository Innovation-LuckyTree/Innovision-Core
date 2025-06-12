namespace Innovision.Core.Infrastructure.CoreIdentity.Models.Responses;

public class CreateUserResponse
{
    public Guid Id { get; set; }
    public int IdNumber { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
}
