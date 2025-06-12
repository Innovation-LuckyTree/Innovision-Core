using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;

namespace Innovision.Core.Application.Common.Interfaces;

public interface IAccountServices
{
    public Account GenerateCreateUserModel(Details details, Guid userId, bool IsActive, bool IsMain, ICurrentUserService _currentUserService);
    public string GenerateCode(int string_length);
}
