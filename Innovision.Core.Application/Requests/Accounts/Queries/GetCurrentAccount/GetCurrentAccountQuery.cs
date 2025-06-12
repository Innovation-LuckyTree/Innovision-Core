using Innovision.Core.Application.Common.Models.Responses;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccount
{
    public class GetCurrentAccountQuery : IRequest<CurrentAccountResponse> { }
}
