using MediatR;

namespace Innovision.Core.Application.Requests.SelfExclusion.Commands.CreateNewExclusion;

public class CreateNewExclusionCommand : IRequest<SelfExclusionDto>
{
    public long AccountId { get; set; }
    public  DateTimeOffset? DateStart { get; set; }
    public  DateTimeOffset? DateEnd { get; set; }
    public bool IsIndefinite { get; set; }
}