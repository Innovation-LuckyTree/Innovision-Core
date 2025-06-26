using Innovision.Core.Application.Requests.Orders.Commands.AddItemOrder;
using Innovision.Core.Application.Requests.Orders.Commands.RevertBetTransactions;
using Innovision.Core.Application.Requests.Orders.Commands.ScheduleBetTransactions;
using Innovision.Core.Application.Requests.Orders.Queries.GetOrderedByIdItems;
using Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionDetail;
using Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactions;
using Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsByIds;
using Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsList;
using Innovision.Core.Application.Requests.Orders.Queries.GetOrders;
using Innovision.Core.Application.Requests.Orders.Queries.GetOrdersByGame;
using Innovision.Core.Application.Requests.Orders.Queries.GetPagedOrders;
using Innovision.Core.Application.Requests.Orders.Queries.GetUserCurrentUnusedItems;
using Innovision.Core.Application.Requests.Orders.Queries.GetUserUnusedItems;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class BetTransactionController : ApiBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAccountOrders(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetOrdersQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpPost("items")]
    public async Task<IActionResult> GetBetTransactions(GetBetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("paged-list")]
    public async Task<IActionResult> GetPagedOrdersQuery(GetPagedOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("item/detail/{BetTransactionId}")]
    public async Task<IActionResult> GetBetTransactionDetail(long BetTransactionId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBetTransactionDetailQuery(BetTransactionId), cancellationToken);

        return Ok(result);
    }

    [HttpPost("item/detail/list")]
    public async Task<IActionResult> GetBetTransactionList(GetBetTransactionsByIdsQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("item/detail/{BetTransactionId}/{size}")]
    public async Task<IActionResult> GetBetTransactionDetail(long BetTransactionId, int size, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBetTransactionsListQuery(BetTransactionId, size), cancellationToken);

        return Ok(result);
    }

    [HttpGet("game/{gameId}")]
    public async Task<IActionResult> GetOrdersByGame(int gameId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetOrdersByGameQuery(gameId), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrders(long orderId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetOrderedByIdItemsQuery(orderId), cancellationToken);

        return Ok(result);
    }

    // [HttpGet("order/{orderId}")]
    // public async Task<IActionResult> GetOrderDetail(long orderId, CancellationToken cancellationToken)
    // {
    //     var result = await Mediator.Send(new GetOrderDetailQuery(orderId), cancellationToken);

    //     return Ok(result);
    // }

    [HttpGet("unused/{gameId}")]
    public async Task<IActionResult> GetUnusedOrders(int gameId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUserUnusedItemsQuery(gameId), cancellationToken);

        return Ok(result);
    }

    [HttpGet("unused/{gameId}/current")]
    public async Task<IActionResult> GetCurrentUnusedOrders(int gameId, DateTime OpenSchedule, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUserCurrentUnusedItemsQuery(gameId, OpenSchedule), cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder(long orderId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteOrdersCommand(orderId), cancellationToken);

        return Ok(result);
    }

    [HttpPost("schedule")]
    public async Task<IActionResult> ScheduleOrderedItems(ScheduleBetTransactionsCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("schedule/revert")]
    public async Task<IActionResult> RevertBetTransactions(RevertBetTransactionsCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    // [HttpPost("migrate/orders/range")]
    // public async Task<IActionResult> GetPlayersMigrateItems(GetAccountOrdersRangeQuery request, CancellationToken cancellationToken)
    // {
    //     var result = await Mediator.Send(request, cancellationToken);

    //     return Ok(result);
    // }
}