using Nop.Plugin.Widgets.Parvez.Domain;
using Nop.Plugin.Widgets.Parvez.Models;

namespace Nop.Plugin.Widgets.Parvez.Factories
{
    public interface IBsEmployeeModelFactory
    {
        Task<EmployeePublicModel> PrepareEmployeeModelFactoryAsync(BsEmployee employee);
        Task<IList<EmployeePublicModel>> PrepareEmployeeListModelFactoryAsync(IList<BsEmployee> employees);
    }
}
