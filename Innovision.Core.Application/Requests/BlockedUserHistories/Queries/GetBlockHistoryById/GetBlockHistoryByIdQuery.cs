using MediatR;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetBlockHistoryById;

public record GetBlockHistoryByIdQuery(int BlockedUserHistoryId) : IRequest<BlockUserDto>;
