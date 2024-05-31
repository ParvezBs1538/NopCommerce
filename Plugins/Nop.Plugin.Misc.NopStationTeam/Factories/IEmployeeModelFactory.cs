using Nop.Plugin.Misc.NopStationTeam.Domain;
using Nop.Plugin.Misc.NopStationTeam.Models;

namespace Nop.Plugin.Misc.NopStationTeam.Factories
{
    public interface IEmployeeModelFactory
    {
        Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel);

        Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel);

        Task<EmployeeModel> PrepareEmployeeModelAsync(EmployeeModel model, Employee employee, bool excludeProperties = false);
    }
}