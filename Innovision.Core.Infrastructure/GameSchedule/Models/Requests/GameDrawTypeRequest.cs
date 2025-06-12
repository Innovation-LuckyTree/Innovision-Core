namespace Innovision.Core.Infrastructure.GameSchedule.Models.Requests;

public class GameDrawTypeRequest
{
    public string CompanyId { get; set; }
    public string GameDrawTypeName { get; set; }
    public string OpenSchedule { get; set; }
    public string EndCutOff { get; set; }
    public string DrawTime { get; set; }
    public bool IsDeleted { get; set; } = false;
}
