using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetBranchById;

public record GetBranchByIdQuery(int BranchId) : IRequest<ApiResponse<BranchDetailsDto>>;
