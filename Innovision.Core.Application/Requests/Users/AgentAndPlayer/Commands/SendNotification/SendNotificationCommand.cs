using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Commands.SendNotification;

public record SendNotificationCommand(long CompanyId) : IRequest<Unit>;
