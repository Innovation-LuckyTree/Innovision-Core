using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class FrequentlyAskQuestion : AuditableEntity
{
    public int FrequentlyAskQuestionId { get; set; }
    public int GameId { get; set; }
    public int IsApplicationRelated { get; set; }
    public int OrderNo { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }

    public virtual Game Game { get; set; }
}
