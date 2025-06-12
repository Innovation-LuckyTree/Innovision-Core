namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayersUnusedQuery;

public class GetPlayersUnusedVM
{
    
    public int Total { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public List<GetPlayersUnusedDto> Players { get; set; }
}