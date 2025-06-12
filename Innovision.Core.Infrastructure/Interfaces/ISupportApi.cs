using Innovision.Core.Infrastructure.Support.Models.Response;

namespace Innovision.Core.Infrastructure.Interfaces;

public interface ISupportApi
{
    Task<GetCasesResponse> GetCases(GetCasesRequest request, CancellationToken cancellationToken);
}
