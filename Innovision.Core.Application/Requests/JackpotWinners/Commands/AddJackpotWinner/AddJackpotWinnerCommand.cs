using Innovision.Core.Application.Requests.JackpotWinners.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.JackpotWinners.Commands.AddJackpotWinner;

public class AddJackpotWinnerCommand : IRequest<JackpotWinnerDto>
{
    public long AccountInfoId { get; set; }
    public string BetValue { get; set; }
    public int CompanyGameId { get; set; }
    public string TransactionNo { get; set; }
    public long DrawResultId { get; set; }
    public string GameTypeName { get; set; }
    public int GameId { get; set; }
    public string DrawResult { get; set; }
    public long BetTransactionId { get; set; }
    public long GameScheduleId { get; set; }
    public  DateTimeOffset DrawDate { get; set; }
    public TimeSpan DrawTime { get; set; }
    public decimal PrizePoolAmount { get; set; }
    public decimal NetWinAmount { get; set; }
    public decimal GrossWinAmount { get; set; }
    public int NumberOfWinners { get; set; }
    public decimal TotalBetAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TaxPercentage { get; set; }
    public long? ApproverAccountId { get; set; }
    public long? ReleaserAccountId { get; set; }
    public int JackpotWinnerStatusId { get; set; }
    public string Remarks { get; set; }
}
