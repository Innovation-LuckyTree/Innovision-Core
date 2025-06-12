using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class Order : AuditableEntity
{
    public Order()
    {
        OrderItems = new HashSet<OrderItem>();
    }

    public long OrderId { get; set; }
    public int GameId { get; set; }
    public long AccountInfoId { get; set; }
    public string TransactionNo { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalNoOfItems { get; set; }
    public int CommissionStatusId { get; set; } = 1;
    public bool IsBonus { get; set; } = false;
    public bool IsDeleted { get; set; } = false;

    public virtual Account AccountInfo { get; set; }
    public virtual Game Game { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}
