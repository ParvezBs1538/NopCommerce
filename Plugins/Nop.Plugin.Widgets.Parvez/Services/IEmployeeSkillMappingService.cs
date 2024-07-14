using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Services
{
    public interface IEmployeeSkillMappingService
    {
        Task InsertEmployeeSkillMappingAsync(EmployeeSkillMapping EmployeeSkillMapping);

        Task UpdateEmployeeSkillMappingAsync(EmployeeSkillMapping EmployeeSkillMapping);

        Task<IList<EmployeeSkillMapping>> GetEmployeeSkillMappingsByEmployeeIdAsync(int EmployeeId);

        Task<EmployeeSkillMapping> FindEmployeeSkillMappingAsync(int EmployeeId, int skillId);
    }
}
