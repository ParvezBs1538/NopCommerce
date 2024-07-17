using Nop.Plugin.Widgets.Parvez.Domain;
using Nop.Plugin.Widgets.Parvez.Models;

namespace Nop.Plugin.Widgets.Parvez.Factories
{
    public class EmployeeModelFactory : IEmployeeModelFactory
    {
        public async Task<IList<EmployeePublicModel>> PrepareEmployeePublicListModelAsync(IList<Employee> employees)
        {
            var model = new List<EmployeePublicModel>();

            foreach (var employee in employees)
                model.Add(await PrepareEmployeePublicModelAsync(employee));

            return model;
        }

        public async Task<EmployeePublicModel> PrepareEmployeePublicModelAsync(Employee employee)
        {
            return new EmployeePublicModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                IsMVP = employee.IsMVP,
                IsCertified = employee.IsCertified,
                EmployeeStatus = employee.EmployeeStatus,
                EmployeeStatusStr = employee.EmployeeStatus.ToString(),
            };
        }
    }
}
