using Nop.Core.Configuration;

namespace Nop.Plugin.Payments.BdPay
{
    public class BdPayPaymentSettings : ISettings
    {
        public TransactMode TransactMode { get; set; }

        public bool AdditionalFeePercentage { get; set; }

        public decimal AdditionalFee { get; set; }
    }
}
