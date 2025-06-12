namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayersMigrateRange;

public class GetPlayerMigrateRangeVM
{
    
    public int Total { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public List<PlayerMigrateAccountDto> Players { get; set; }
}