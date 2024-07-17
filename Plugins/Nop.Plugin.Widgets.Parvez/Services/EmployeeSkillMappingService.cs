using Nop.Data;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Services
{
    public class EmployeeSkillMappingService : IEmployeeSkillMappingService
    {
        private readonly IRepository<EmployeeSkillMapping> _employeeSkillMappingRepository;

        public EmployeeSkillMappingService(IRepository<EmployeeSkillMapping> employeeSkillMappingRepository)
        {
            _employeeSkillMappingRepository = employeeSkillMappingRepository;
        }

        public async Task InsertEmployeeSkillMappingAsync(EmployeeSkillMapping employeeSkillMapping)
        {
            await _employeeSkillMappingRepository.InsertAsync(employeeSkillMapping);
        }

        public async Task UpdateEmployeeSkillMappingAsync(EmployeeSkillMapping employeeSkillMapping)
        {
            await _employeeSkillMappingRepository.UpdateAsync(employeeSkillMapping);
        }

        public async Task DeleteEmployeeSkillMappingAsync(EmployeeSkillMapping employeeSkillMapping)
        {
            await _employeeSkillMappingRepository.DeleteAsync(employeeSkillMapping);
        }

        public async Task<EmployeeSkillMapping> FindEmployeeSkillMappingAsync(int employeeId, int skillId)
        {
            return await _employeeSkillMappingRepository.Table
                .FirstOrDefaultAsync(ds => ds.SkillId == skillId && ds.EmployeeId == employeeId);
        }

        public async Task<IList<EmployeeSkillMapping>> GetEmployeeSkillMappingsByEmployeeIdAsync(int employeeId)
        {
            return await _employeeSkillMappingRepository.Table
                .Where(ds => ds.EmployeeId == employeeId)
                .ToListAsync();
        }
    }
}
