using MediatR;

namespace Innovision.Core.Application.Requests.Games.Commands.AddGameType;

public class AddGameTypeCommand : IRequest<Unit>
{
    public int GameId { get; set; }
    public int GameReferenceId { get; set; } // from game api
    public string GameTypeName { get; set; }
    public string GameTypeDesciption { get; set; }
}
