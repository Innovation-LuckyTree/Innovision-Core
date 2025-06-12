using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.Support.Models.Response;
using MediatR;

namespace Innovision.Core.Application.Requests.Support.Queries.GetCaseItems;

public class GetCaseItemsQueryHandler : IRequestHandler<GetCaseItemsQuery, GetCasesResponse>
{
    private readonly ISupportApi _supportApi;

    public GetCaseItemsQueryHandler(ISupportApi supportApi)
    {
        _supportApi = supportApi;
    }

    public async Task<GetCasesResponse> Handle(GetCaseItemsQuery request, CancellationToken cancellationToken)
    {
        var response = await _supportApi.GetCases(new GetCasesRequest
        {
            OrganizationId = request.OrganizationId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PagedQuery = request.PagedQuery
        }, cancellationToken);

        return response;
    }
}