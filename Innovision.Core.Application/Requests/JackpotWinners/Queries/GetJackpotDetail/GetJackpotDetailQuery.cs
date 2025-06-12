using MediatR;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotDetail;

public record GetJackpotDetailQuery(long JackpotWinnerId) : IRequest<JackpotWinnerDto>;
