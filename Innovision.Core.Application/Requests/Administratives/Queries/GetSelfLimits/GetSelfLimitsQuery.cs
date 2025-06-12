using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetSelfLimits;

public record GetSelfLimitsQuery(PagedQuery PagedQuery, int? Status = 1) : IRequest<SelfLimitVm>;
