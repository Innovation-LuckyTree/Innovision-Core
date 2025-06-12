using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAdminExclusions;

public record GetAdminExclusionsQuery(PagedQuery PagedQuery, int? Status = 1) : IRequest<AdministrativeExclusionVm>;
