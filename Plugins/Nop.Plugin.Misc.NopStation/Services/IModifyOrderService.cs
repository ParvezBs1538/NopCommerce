using Nop.Core;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Services;

public interface IModifyOrderService
{
    Task DeleteModifyOrderAsync(ModifyOrder modifyOrder);
    
    Task<IPagedList<ModifyOrder>> GetAllModifyOrdersAsync(int orderId = 0, int customerId = 0, 
        int pageIndex = 0, int pageSize = int.MaxValue);
    
    Task<ModifyOrder> GetModifyOrderByIdAsync(int modifyOrderId);
    Task<ModifyOrder> GetModifyOrderByOrderIdAsync(int orderId);
    Task InsertModifyOrderAsync(ModifyOrder modifyOrder);
    
    Task UpdateModifyOrderAsync(ModifyOrder modifyOrder);
}
