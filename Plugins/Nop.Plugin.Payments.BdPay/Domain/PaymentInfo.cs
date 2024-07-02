using Nop.Core;

namespace Nop.Plugin.Payments.BdPay.Domain
{
    public class PaymentInfo : BaseEntity
    {
        public string AccountType { get; set; }

        public string MobileNumber { get; set; }

        public string TransactionId { get; set; }

        public int CustomerId { get; set; }

        public int OrderId { get; set; }

        public decimal TotalTk { get; set; }
    }
}
