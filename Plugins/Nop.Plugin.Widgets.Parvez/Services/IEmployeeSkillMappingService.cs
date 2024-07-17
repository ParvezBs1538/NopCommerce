using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Services
{
    public interface IEmployeeSkillMappingService
    {
        Task InsertEmployeeSkillMappingAsync(EmployeeSkillMapping employeeSkillMapping);

        Task UpdateEmployeeSkillMappingAsync(EmployeeSkillMapping employeeSkillMapping);

        Task DeleteEmployeeSkillMappingAsync(EmployeeSkillMapping employeeSkillMapping);

        Task<IList<EmployeeSkillMapping>> GetEmployeeSkillMappingsByEmployeeIdAsync(int employeeId);

        Task<EmployeeSkillMapping> FindEmployeeSkillMappingAsync(int employeeId, int skillId);
    }
}
