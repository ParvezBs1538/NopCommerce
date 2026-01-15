using Nop.Plugin.Misc.NopStation.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Factories
{
    public interface IEmployeeModelFactory
    {
        Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel);

        Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel);

        Task<EmployeeModel> PrepareEmployeeModelAsync(EmployeeModel model, Employee employee, bool excludeProperties = false);
    }
}
