using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Payments.BdPay.Models
{
    public record PaymentInfoModel : BaseNopModel
    {
        public PaymentInfoModel()
        {
            AccountTypes = [];
        }

        [NopResourceDisplayName("Payment.SelectAccount")]
        public string AccountType { get; set; }

        [NopResourceDisplayName("Payment.SelectAccount")]
        public IList<SelectListItem> AccountTypes { get; set; }

        [NopResourceDisplayName("Payment.MobileNumber")]
        public string MobileNumber { get; set; }

        [NopResourceDisplayName("Payment.TransactionId")]
        public string TransactionId { get; set; }

        [NopResourceDisplayName("Payment.CustomerId")]
        public int CustomerId { get; set; }

        [NopResourceDisplayName("Payment.OrderId")]
        public int OrderId { get; set; }

		[NopResourceDisplayName("Payment.TotalTk")]
		public decimal TotalTk { get; set; }
	}
}
