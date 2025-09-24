using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.NopStation.Models;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Services.Customers;
using Nop.Services.Helpers;

namespace Nop.Plugin.Misc.NopStation.Factories;

public class ModifyOrderModelFactory : IModifyOrderModelFactory
{
    #region Fields

    private readonly ICustomerService _customerService;
    private readonly IDateTimeHelper _dateTimeHelper;
    private readonly IModifyOrderService _modifyOrderService;

    #endregion

    #region Ctor

    public ModifyOrderModelFactory(ICustomerService customerService,
        IDateTimeHelper dateTimeHelper,
        IModifyOrderService modifyOrderService)
    {
        _customerService = customerService;
        _dateTimeHelper = dateTimeHelper;
        _modifyOrderService = modifyOrderService;
    }

    #endregion

    #region Methods

    public async Task<ModifyOrderModel> PrepareModifyOrderModelAsync(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        var modifyOrder = await _modifyOrderService.GetModifyOrderByOrderIdAsync(order.Id);
        if (modifyOrder == null)
            return new ModifyOrderModel();

        var model = new ModifyOrderModel
        {
            OrderId = order.Id,
            CreatedOn = await _dateTimeHelper.ConvertToUserTimeAsync(modifyOrder.CreatedOnUtc, DateTimeKind.Utc),
            UpdatedOn = await _dateTimeHelper.ConvertToUserTimeAsync(modifyOrder.UpdatedOnUtc, DateTimeKind.Utc),
        };

        var customer = await _customerService.GetCustomerByIdAsync(modifyOrder.CustomerId);
        model.CustomerName = await _customerService.GetCustomerFullNameAsync(customer);

        return model;
    }

    public async Task<IList<ModifyOrderModel>> PrepareFooterBeforeModelAsync()
    {
        var models = new List<ModifyOrderModel>();

        var modifyOrders = await _modifyOrderService.GetAllModifyOrdersAsync();
        foreach (var modifyOrder in modifyOrders)
        {
            var model = new ModifyOrderModel
            {
                OrderId = modifyOrder.OrderId,
                CreatedOn = await _dateTimeHelper.ConvertToUserTimeAsync(modifyOrder.CreatedOnUtc, DateTimeKind.Utc),
                UpdatedOn = await _dateTimeHelper.ConvertToUserTimeAsync(modifyOrder.UpdatedOnUtc, DateTimeKind.Utc),
            };

            var customer = await _customerService.GetCustomerByIdAsync(modifyOrder.CustomerId);
            model.CustomerName = await _customerService.GetCustomerFullNameAsync(customer);

            models.Add(model);
        }

        return models;
    }

    #endregion
}
