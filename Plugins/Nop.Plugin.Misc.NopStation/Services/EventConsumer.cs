using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Services.Events;

namespace Nop.Plugin.Misc.NopStation.Services;

// ref (vitalac): namespace NopStation.Plugin.SMS.GrameenPhone.Infrastructure;
public class EventConsumer : IConsumer<OrderPlacedEvent>
{
    #region Fields

    private readonly IModifyOrderService _modifyOrderService;

    #endregion

    #region Ctor

    public EventConsumer(IModifyOrderService modifyOrderService)
    {
        _modifyOrderService = modifyOrderService;
    }

    #endregion

    public async Task HandleEventAsync(OrderPlacedEvent eventMessage)
    {
        var order = eventMessage.Order;

        var modifyOrder = new ModifyOrder
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            CreatedOnUtc = DateTime.UtcNow,
            UpdatedOnUtc = DateTime.UtcNow,
        };

        await _modifyOrderService.InsertModifyOrderAsync(modifyOrder);

        // TODO: send SMS
    }
}
