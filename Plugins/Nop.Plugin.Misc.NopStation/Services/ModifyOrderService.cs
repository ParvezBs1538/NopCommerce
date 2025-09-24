using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Services;

public class ModifyOrderService : IModifyOrderService
{
    private readonly IRepository<ModifyOrder> _modifyOrderRepository;

    public ModifyOrderService(IRepository<ModifyOrder> modifyOrderRepository)
    {
        _modifyOrderRepository = modifyOrderRepository;
    }

    public virtual async Task InsertModifyOrderAsync(ModifyOrder modifyOrder)
    {
        await _modifyOrderRepository.InsertAsync(modifyOrder);
    }

    public virtual async Task UpdateModifyOrderAsync(ModifyOrder modifyOrder)
    {
        await _modifyOrderRepository.UpdateAsync(modifyOrder);
    }

    public virtual async Task DeleteModifyOrderAsync(ModifyOrder modifyOrder)
    {
        await _modifyOrderRepository.DeleteAsync(modifyOrder);
    }

    public virtual async Task<ModifyOrder> GetModifyOrderByIdAsync(int modifyOrderId)
    {
        return await _modifyOrderRepository.GetByIdAsync(modifyOrderId);
    }

    public virtual async Task<ModifyOrder> GetModifyOrderByOrderIdAsync(int orderId)
    {
        var query = from mo in _modifyOrderRepository.Table
                    where mo.OrderId == orderId
                    select mo;

        return await query.FirstOrDefaultAsync();
    }

    public virtual async Task<IPagedList<ModifyOrder>> GetAllModifyOrdersAsync(int orderId = 0, int customerId = 0,
        int pageIndex = 0, int pageSize = int.MaxValue)
    {
        var query = _modifyOrderRepository.Table;

        if (orderId > 0)
            query = query.Where(mo => mo.OrderId == orderId);

        if (customerId > 0)
            query = query.Where(mo => mo.CustomerId == customerId);

        query = query.OrderByDescending(mo => mo.Id);

        return await query.ToPagedListAsync(pageIndex, pageSize);
    }
}
