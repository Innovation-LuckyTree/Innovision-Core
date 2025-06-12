using Innovision.Core.Application.Requests.JackpotWinners.Commands.AddJackpotWinner;
using Innovision.Core.Application.Requests.JackpotWinners.Queries.GetCurrentAccountJackpotWin;
using Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotDetail;
using Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotDetailsByOrder;
using Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerList;
using Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerListAll;
using Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerListExport;
using Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnersByGame;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class JackpotWinnerController : ApiBaseController
{
    [HttpGet("detail/{jackpotWinnerId}")]
    public async Task<IActionResult> GetJackpotDetail(long jackpotWinnerId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetJackpotDetailQuery(jackpotWinnerId), cancellationToken);

        return Ok(result);
    }

    [HttpPost("account/current")]
    public async Task<IActionResult> GetCurrentAccountJackpotWin([FromBody] GetCurrentAccountJackpotWinQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("{companyGameId}/list")]
    public async Task<IActionResult> GetJackpotWinnerList([FromBody] GetJackpotWinnerListRequest requestBody, long companyGameId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetJackpotWinnerListQuery(requestBody, companyGameId), cancellationToken);

        return Ok(result);
    }

    [HttpPost("{companyGameId}/list/export")]
    public async Task<IActionResult> GetJackpotWinnerListExport(GetJackpotWinnerListExportQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("list/all")]
    public async Task<IActionResult> GetJackpotWinnerListAll([FromBody] GetJackpotWinnerListAllQuery request, long companyGameId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("schedule/{gameScheduleId}")]
    public async Task<IActionResult> GetJackpotWinnersByGameSchedule(long gameScheduleId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetJackpotWinnersByGameScheduleQuery(gameScheduleId), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddJackpotWinner(AddJackpotWinnerCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("list/details")]
    public async Task<IActionResult> GetJackpotDetails(GetJackpotDetailsByOrderQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateJackpotWinner(UpdateJackpotWinnerCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
