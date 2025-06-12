namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetSelfLimits;

public record SelfLimitVm(IEnumerable<SelfLimitDto> SelfLimits)
{
    public int Total { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Count
    {
        get => SelfLimits?.Count() ?? 0;
    }
}
