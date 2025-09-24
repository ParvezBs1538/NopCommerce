using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.NopStation.Models;

namespace Nop.Plugin.Misc.NopStation.Factories;

public interface IModifyOrderModelFactory
{
    Task<IList<ModifyOrderModel>> PrepareFooterBeforeModelAsync();
    Task<ModifyOrderModel> PrepareModifyOrderModelAsync(Order order);
}
