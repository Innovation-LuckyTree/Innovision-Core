namespace Innovision.Core.Application.Requests.PaymentMethods.Queries.GetPaymentMethods;

public record PaymentMethodVm(IEnumerable<PaymentMethodDto> PaymentMethods)
{
    public int Count
    {
        get
        {
            return PaymentMethods.Count();
        }
    }
}

