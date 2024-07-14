using Nop.Plugin.Widgets.Parvez.Domain;
using Nop.Plugin.Widgets.Parvez.Models;

namespace Nop.Plugin.Widgets.Parvez.Factories
{
    public interface IEmployeeModelFactory
    {
        Task<EmployeePublicModel> PrepareEmployeeModelFactoryAsync(Employee employee);
        Task<IList<EmployeePublicModel>> PrepareEmployeeListModelFactoryAsync(IList<Employee> employees);
    }
}
