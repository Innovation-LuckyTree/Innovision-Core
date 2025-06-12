namespace Innovision.Core.Infrastructure.CoreIdentity.Models.Requests;

public record UpdateUserPasswordRequest(Guid UserId, string NewPassword, string ConfirmNewPassword);
