namespace Innovision.Core.Infrastructure.Games.Models.Responses;

public class CompanyGameResponse
{
    public long Id { get; set; }
    public Guid CompanyId { get; set; }
    public string GameSettings { get; set; }
    public string Livestream { get; set; }
    public bool IsDeleted { get; set; }
    public long Game { get; set; }
}
