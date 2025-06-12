namespace Innovision.Core.Common.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; }
    string AuthenticationBearer { get; }
    int RoleId { get; set; }
    string RoleName { get; set; }
    string UserName { get; set; }
    Guid UserObjId { get; set; }
    string CompanyId { get; }
    Guid? CompanyObjectId { get; }
}