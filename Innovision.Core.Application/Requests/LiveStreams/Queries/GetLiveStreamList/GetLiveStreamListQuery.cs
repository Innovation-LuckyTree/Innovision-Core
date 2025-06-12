using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.LiveStreams.Queries.GetLiveStreamList;

public class GetLiveStreamListQuery : IRequest<LiveStreamVm>
{
    public PagedQuery PagedQuery { get; set; }
}
