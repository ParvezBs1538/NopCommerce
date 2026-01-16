using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;

namespace Nop.Plugin.Misc.NopStation.Factories;

public interface IEmployeeModelFactory
{
    Task<IList<EmployeeModel>> PrepareEmployeeListModelAsync(IList<Employee> employees);

    Task<EmployeeModel> PrepareEmployeeModelAsync(Employee employee);
}
