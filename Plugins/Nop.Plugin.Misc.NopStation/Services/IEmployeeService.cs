using Nop.Core;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Services
{
    public interface IEmployeeService
    {
        Task InsertEmployeeAsync(Employee employee);

        Task UpdateEmployeeAsync(Employee employee);

        Task DeleteEmployeeAsync(Employee employee);

        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        Task<IPagedList<Employee>> SearchEmployeesAsync(string name, int? statusId = null, int? designationId = null,
            bool? isMvp = null, bool? isCert = null, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
