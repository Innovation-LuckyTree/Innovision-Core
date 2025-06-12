namespace Innovision.Core.Infrastructure.AccountServices.Models.Requests
{
    public class WithdrawBalanceRequest
    {
        public Guid AccountId { get; set; }
        public string TransactionNo { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public string ModeOfTransaction { get; set; }
    }
}
