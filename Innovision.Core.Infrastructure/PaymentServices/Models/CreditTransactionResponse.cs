namespace Innovision.Core.Infrastructure.PaymentServices.Models
{
    public class CreditTransactionResponse
    {
        public long Id { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public Guid SenderCreditId { get; set; }
        public Guid ReceiverCreditId { get; set; }
        public int TransType { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
    }
}
