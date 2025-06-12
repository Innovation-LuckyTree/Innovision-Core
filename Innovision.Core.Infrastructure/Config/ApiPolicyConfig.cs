namespace Innovision.Core.Infrastructure.Config;

public class ApiPolicyConfig
{
    public HttpCircuitBreaker HttpCircuitBreaker { get; set; }
    public HttpRetry HttpRetry { get; set; }    
}
