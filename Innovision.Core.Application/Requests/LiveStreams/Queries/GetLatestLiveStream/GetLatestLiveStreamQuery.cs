using MediatR;

namespace Innovision.Core.Application.Requests.LiveStreams.Queries.GetLatestLiveStream;

public record GetLatestLiveStreamQuery(int GameId) : IRequest<LiveStreamDto>;
