namespace Innovision.Core.Infrastructure.Config;

public class HttpCircuitBreaker
{
    public string DurationOfBreak { get; set; }
    public int ExceptionsAllowedBeforeBreaking { get; set; }
}
