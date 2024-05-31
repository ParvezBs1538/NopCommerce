using Nop.Data;
using Nop.Plugin.Misc.NopStationTeam.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.NopStationTeam.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public virtual void Log(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            _employeeRepository.Insert(employee);
        }
    }
}
