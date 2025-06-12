using MediatR;

namespace Innovision.Core.Application.Requests.Games.Commands.CreateGame;

public record CreateGameCommand(string Name, string Description) : IRequest<Unit>;
