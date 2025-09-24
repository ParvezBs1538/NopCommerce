using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.NopStation.Factories;
using Nop.Services.Orders;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Order;

namespace Nop.Plugin.Misc.NopStation.Components;

public class ModifyOrderViewComponent : NopViewComponent
{
    #region Fields

    private readonly IOrderService _orderService;
    private readonly IModifyOrderModelFactory _modifyOrderModelFactory;

    #endregion

    #region Ctor

    public ModifyOrderViewComponent(IOrderService orderService,
        IModifyOrderModelFactory modifyOrderModelFactory)
    {
        _orderService = orderService;
        _modifyOrderModelFactory = modifyOrderModelFactory;
    }

    #endregion

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        if (additionalData is not OrderDetailsModel orderDetailsModel)
            return Content("");

        var order = await _orderService.GetOrderByIdAsync(orderDetailsModel.Id);
        if (order is null || order.Deleted)
            return Content("");

        var model = await _modifyOrderModelFactory.PrepareModifyOrderModelAsync(order);

        return View("~/Plugins/Misc.NopStation/Views/Shared/Components/ModifyOrder/Default.cshtml", model);

        //return View(model);
    }
}
