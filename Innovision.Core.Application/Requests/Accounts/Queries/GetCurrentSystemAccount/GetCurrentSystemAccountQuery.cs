using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentSystemUser;

public class GetCurrentSystemUserQuery : IRequest<SystemAccountInfoDto>;
