using Nop.Core;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Services
{
    public interface ISkillService
    {
        Task InsertSkillAsync(Skill skill);

        Task UpdateSkillAsync(Skill skill);

        Task DeleteSkillAsync(Skill skill);

        Task<Skill> GetSkillByIdAsync(int skill);

        Task<IList<Skill>> GetSkillByIdsAsync(int[] skillIds);

        Task<IPagedList<Skill>> SearchSkillsAsync(string name = null,
                int pageIndex = 0, int pageSize = int.MaxValue);


        Task InsertDeveloperSkillMappingAsync(DeveloperSkillMapping developerSkillMapping);

        Task UpdateDeveloperSkillMappingAsync(DeveloperSkillMapping developerSkillMapping);

        Task DeleteDeveloperSkillMappingAsync(DeveloperSkillMapping developerSkillMapping);
        
        Task<IList<DeveloperSkillMapping>> GetDeveloperSkillMappingsByDeveloperIdAsync(int developerId);
        
        Task<DeveloperSkillMapping> GetDeveloperSkillMappingByIdAsync(int developerSkillMappingId);

        Task<DeveloperSkillMapping> FindDeveloperSkillMappingAsync(int developerId, int skillId);

        Task<IList<Skill>> GetAllSkillsAsync();
    }
}
