namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionDetail;

public class OrderGrossDailyDto
{
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal DeckAmount { get; set; }
    public decimal AdvanceAmount { get; set; }
    public decimal GrossAmount { get; set; }
    public int TotalCount { get; set; }
    public DateTime Date { get; set; }
    public decimal RegularAmount
    {
        get
        {
            return GrossAmount - DeckAmount - AdvanceAmount;
        }
    }
}

public class OrderGrossMonthlyDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal DeckAmount { get; set; }
    public decimal AdvanceAmount { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal RegularAmount
    {
        get
        {
            return GrossAmount - DeckAmount - AdvanceAmount;
        }
    }

    public IEnumerable<OrderGrossDailyDto> DailyGross { get; set; }
}