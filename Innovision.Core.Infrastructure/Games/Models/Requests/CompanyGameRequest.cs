namespace Innovision.Core.Infrastructure.Games.Models.Requests;

public class CompanyGameRequest
{
    public int Game { get; set; } = 0;
    public string CompanyId { get; set; }
    public string GameSettings { get; set; }
    public string LiveStream { get; set; }
}
