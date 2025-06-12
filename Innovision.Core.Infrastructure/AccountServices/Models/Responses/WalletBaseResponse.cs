namespace Innovision.Core.Infrastructure.AccountServices.Models.Responses;

public class WalletBaseResponse<T>
{
    public string Status { get; set; } = "success";
    public ProviderBaseResponse<T> Data { get; set; }
    public string ErrorMessage { get; set; }
}

public class ProviderBaseResponse<T>
{
    public bool Ok { get; set; }
    public object Pagination { get; set; }
    public T Data { get; set; }
}