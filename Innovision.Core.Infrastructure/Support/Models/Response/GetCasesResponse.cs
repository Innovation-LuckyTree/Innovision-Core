namespace Innovision.Core.Infrastructure.Support.Models.Response;

public record GetCasesResponse(IEnumerable<CaseDto> Cases, int total, int pageNum, int? rowPerPage);