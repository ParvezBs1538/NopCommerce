using Nop.Plugin.Widgets.Parvez.Areas.Admin.Models;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Areas.Admin.Factories
{
    public interface IEmployeeModelFactory
    {
        Task<EmployeeModel> PrepareEmployeeModelAsync(EmployeeModel model, BsEmployee employee, bool excludeProperties = false);
        Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel);
        Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel);
    }
}
