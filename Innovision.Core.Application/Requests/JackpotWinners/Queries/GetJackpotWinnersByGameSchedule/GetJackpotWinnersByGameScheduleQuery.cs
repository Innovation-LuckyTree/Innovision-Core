using MediatR;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnersByGame;

public record GetJackpotWinnersByGameScheduleQuery(long GameScheduleId) : IRequest<JackpotWinnerInfoVm>;
