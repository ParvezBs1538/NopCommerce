using Nop.Plugin.Payments.BdPay.Domain;

namespace Nop.Plugin.Payments.BdPay.Services
{
    public interface IPaymentInfoServices
    {
        Task InsertPaymentInfoAsync(PaymentInfo paymentInfo);
    }
}
