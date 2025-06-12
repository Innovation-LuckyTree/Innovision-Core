namespace Innovision.Core.Application.Models.Commission;

public class PlayerCommissionLink
{
    public long CommissionLinkId { get; set; }
    public long MainAgentInfoId { get; set; }
    public decimal MainAgentCommission { get; set; }
    public long AgentInfoId { get; set; }
    public decimal AgentCommission { get; set; }
}
