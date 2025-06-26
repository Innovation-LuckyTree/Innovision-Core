using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.CreateAccountHistory;

public record CreateAccountHistoryCommand(long AccountInfoId, string Action) : IRequest<Unit>;
