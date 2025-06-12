using MediatR;

namespace Innovision.Core.Application.Requests.SelfExclusion.Commands.UpdateCurrentExclusion;

public class UpdateCurrentExclusionCommand : IRequest<SelfExclusionDto>
{
    public int SelfExclusionId { get; set; }
    public  DateTimeOffset? DateStart { get; set; }
    public  DateTimeOffset? DateEnd { get; set; }
    public bool IsIndefinite { get; set; }
    public int Status { get; set; }
}