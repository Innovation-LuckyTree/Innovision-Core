namespace Innovision.Core.Domain.Entity
{
    public class UserTypeConfig
    {
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public bool IsMainUser { get; set; }
        public int? RequestLevel { get; set; }
        public int? CashInLevel { get; set; }
        public string? RequestCredit { get; set; }
        public string? CashinDeposit { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
