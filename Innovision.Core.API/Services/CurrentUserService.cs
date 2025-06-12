using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Innovision.Core.Common.Interfaces;
using Microsoft.Extensions.Primitives;

namespace Innovision.Core.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext == null)
            return;

        var nameIdentifier = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues result))
        {
            if (result.Count > 0)
            {
                AuthenticationBearer = result[0].Replace("Bearer ", "");
            }

            // parse jwt
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(AuthenticationBearer);
            var tokenObject = jsonToken as JwtSecurityToken;

            RoleId = Convert.ToInt32(tokenObject.Claims.First(c => c.Type == "RoleId").Value);
            RoleName = tokenObject.Claims.First(c => c.Type == "role").Value;
            UserName = tokenObject.Claims.First(c => c.Type == "unique_name").Value;
            TenantId = new Guid(tokenObject.Claims.First(c => c.Type == "tenantId").Value);

            if (!string.IsNullOrEmpty(tokenObject.Claims.First(c => c.Type == "user_id")?.Value ?? ""))
            {
                UserObjId = new Guid(tokenObject.Claims.First(c => c.Type == "user_id").Value);
            }
            
            if (!string.IsNullOrEmpty(tokenObject.Claims.First(c => c.Type == "companyId")?.Value ?? ""))
            {
                if (Guid.TryParse(tokenObject.Claims.First(c => c.Type == "companyId").Value, out Guid companyId))
                {
                    CompanyId = tokenObject.Claims.First(c => c.Type == "companyId").Value;
                    CompanyObjectId = companyId;
                }
            }
        }

        if (string.IsNullOrEmpty(nameIdentifier))
        {
            return;
        }

        UserId = nameIdentifier;
    }

    public string UserId { get; }
    public string AuthenticationBearer { get; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string UserName { get; set; }
    public Guid UserObjId { get; set; }
    public Guid TenantId { get; set; }
    public string CompanyId { get; }
    public Guid? CompanyObjectId { get; } = null;
}