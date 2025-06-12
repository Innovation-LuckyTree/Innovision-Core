using MediatR;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerListExport;

public record GetJackpotWinnerListExportQuery(long? JackpotStatusId, long CompanyGameId) : IRequest<JackpotWinnersFile>;