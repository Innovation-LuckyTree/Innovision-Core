using Innovision.Core.Application.Requests.LiveStreams.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.LiveStreams.Commands.CreateLiveStream;

public class CreateLiveStreamCommand : IRequest<LiveStreamDto>
{
    public string Title { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
}
