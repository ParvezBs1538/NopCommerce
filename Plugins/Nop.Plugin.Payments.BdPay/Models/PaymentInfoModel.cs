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

        [NopResourceDisplayName("Payment.Transaction_ID")]
        public string TransactionId { get; set; }
        
    }
}
