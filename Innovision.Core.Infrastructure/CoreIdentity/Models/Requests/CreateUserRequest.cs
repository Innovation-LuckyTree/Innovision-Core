namespace Innovision.Core.Infrastructure.CoreIdentity.Models.Requests;

public record CreateUserRequest(string UserName, string Email, string MobileNumber, string Password, int RoleId, bool IsCompanyAdmin, Guid TenantId, string CompanyId);
