using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Payments.BdPay.Models;
using Nop.Web.Framework.Components;
using Nop.Core.Http.Extensions;

namespace Nop.Plugin.Payments.BdPay.Components;

public class PaymentManualViewComponent : NopViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new PaymentInfoModel()
        {
            AccountTypes = new List<SelectListItem>
            {
                new() { Text = "Bkash", Value = "Bkash" },
                new() { Text = "Nagad", Value = "Nagad" },
                new() { Text = "Rocket", Value = "Rocket" },
                new() { Text = "Upay", Value = "Upay" },
            }
        };

        //set postback values (we cannot access "Form" with "GET" requests)
        if (!Request.IsGetRequest())
        {
            var form = await Request.ReadFormAsync();

            model.MobileNumber = form["MobileNumber"];
            model.TransactionId = form["TransactionId"];
            model.AccountType = form["AccountType"];
        }

        return View("~/Plugins/Payments.BdPay/Views/PaymentInfo.cshtml", model);
    }
}
