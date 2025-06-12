namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemDetail;

public record OrderGrossVm(IEnumerable<OrderGrossMonthlyDto> MonthlyGross)
{
    public decimal TotalDeckAmount
    {
        get => MonthlyGross.Sum(o => o.DeckAmount);
    }

    public decimal TotalAdvanceAmount
    {
        get => MonthlyGross.Sum(o => o.AdvanceAmount);
    }

    public decimal TotalRegularAmount
    {
        get => MonthlyGross.Sum(o => o.RegularAmount);
    }

    public decimal TotalGrossAmount
    {
        get => MonthlyGross.Sum(o => o.GrossAmount);
    }
}
