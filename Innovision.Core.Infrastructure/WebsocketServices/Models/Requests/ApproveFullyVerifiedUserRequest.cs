namespace Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;

public record ApproveFullyVerifiedUserRequest(long AccountId, bool IsFullyVerified);
