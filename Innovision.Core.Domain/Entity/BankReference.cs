namespace Innovision.Core.Domain.Entity
{
    public class BankReference
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<Withdrawal> Withdrawals { get; set; }
    }
}
