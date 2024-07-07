using Nop.Core;
using Nop.Data;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<BsEmployee> _employeeRepository; 

        public EmployeeService(IRepository<BsEmployee> employeeRepository) 
        {
            _employeeRepository = employeeRepository;
        }

        public virtual async Task DeleteEmployeeAsync(BsEmployee employee)
        {
            await _employeeRepository.DeleteAsync(employee);
        }

        public virtual async Task<BsEmployee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _employeeRepository.GetByIdAsync(employeeId);
        }

        public virtual async Task InsertEmployeeAsync(BsEmployee employee)
        {
            await _employeeRepository.InsertAsync(employee);
        }

        public virtual async Task UpdateEmployeeAsync(BsEmployee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
        }

        public virtual async Task<IPagedList<BsEmployee>> SearchEmployeesAsync(string name, int? statusId = null, int? designationId = null,
            bool? isMvp = null, bool? isCert = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _employeeRepository.Table;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            if (statusId > 0)
                query = query.Where(e => e.StatusId == statusId);

            if (designationId > 0)
                query = query.Where(e => e.DesignationId == designationId);
            
            query = query.OrderBy(e => e.DesignationId)
                .ThenByDescending(e => e.IsMVP)
                .ThenByDescending(e => e.IsCertified)
                .ThenBy(e => e.Id);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
