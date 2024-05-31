using Nop.Core;
using Nop.Plugin.Misc.NopStationTeam.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.NopStationTeam.Services;

public interface IEmployeeService
{
    Task InsertEmployeeAsync(Employee employee);

    Task UpdateEmployeeAsync(Employee employee);

    Task DeleteEmployeeAsync(Employee employee);

    Task<Employee> GetEmployeeByIdAsync(int employeeId);

    Task<IPagedList<Employee>> SearchEmployeesAsync(string name, int statusId,
            int pageIndex = 0, int pageSize = int.MaxValue);
}
