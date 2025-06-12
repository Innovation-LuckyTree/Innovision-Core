namespace Innovision.Core.Application.Requests.LiveStreams.Queries;

public record LiveStreamVm(IEnumerable<LiveStreamDto> LiveStreams)
{
    public int Offset { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
}