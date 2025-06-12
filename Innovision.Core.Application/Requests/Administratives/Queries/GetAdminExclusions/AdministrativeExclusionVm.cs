namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAdminExclusions;

public record AdministrativeExclusionVm(IEnumerable<AdministrativeExclusionDto> AdministrativeExclusions)
{
    public int Total { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Count
    {
        get => AdministrativeExclusions?.Count() ?? 0;
    }
}
