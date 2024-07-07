using Nop.Core;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Services
{
    public interface IEmployeeService
    {
        Task InsertEmployeeAsync(BsEmployee employee);
        Task UpdateEmployeeAsync(BsEmployee employee);
        Task DeleteEmployeeAsync(BsEmployee employee);
        Task<BsEmployee> GetEmployeeByIdAsync(int employeeId);
        Task<IPagedList<BsEmployee>> SearchEmployeesAsync(string name, int? statusId = null, int? designationId = null,
            bool? isMvp = null, bool? isCert = null, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
