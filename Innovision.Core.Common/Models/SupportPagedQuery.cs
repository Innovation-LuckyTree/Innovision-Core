namespace Innovision.Core.Common.Models;

public class SupportPagedQuery
{
    public string? Search { get; set; }
    public int Index { get; set; } = 0;
    public int Size { get; set; } = 10;
}
