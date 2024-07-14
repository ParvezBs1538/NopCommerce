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

        public async Task InsertEmployeeSkillMappingAsync(EmployeeSkillMapping EmployeeSkillMapping)
        {
            await _employeeSkillMappingRepository.InsertAsync(EmployeeSkillMapping);
        }

        public async Task UpdateEmployeeSkillMappingAsync(EmployeeSkillMapping EmployeeSkillMapping)
        {
            await _employeeSkillMappingRepository.UpdateAsync(EmployeeSkillMapping);
        }

        public async Task<EmployeeSkillMapping> FindEmployeeSkillMappingAsync(int EmployeeId, int skillId)
        {
            return await _employeeSkillMappingRepository.Table
                .FirstOrDefaultAsync(ds => ds.SkillId == skillId && ds.EmployeeId == EmployeeId);
        }

        public async Task<IList<EmployeeSkillMapping>> GetEmployeeSkillMappingsByEmployeeIdAsync(int EmployeeId)
        {
            return await _employeeSkillMappingRepository.Table
                .Where(ds => ds.EmployeeId == EmployeeId)
                .ToListAsync();
        }
    }
}
