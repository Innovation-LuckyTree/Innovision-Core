using MediatR;

namespace Innovision.Core.Application.Requests.Games.Commands.UpdateGame;

public class UpdateGameCommand : IRequest<Unit>
{
    public int GameId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
