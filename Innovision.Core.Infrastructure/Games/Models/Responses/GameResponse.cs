namespace Innovision.Core.Infrastructure.Games.Models.Responses;

public class GameResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GameMechanics { get; set; }
    public bool IsDeleted { get; set; }
}
