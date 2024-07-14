using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Services
{
    public interface ISkillService
    {
        Task InsertSkillAsync(Skill skill);
        Task UpdateSkillAsync(Skill skill);
        Task DeleteSkillAsync(Skill skill);
        Task<Skill> GetSkillByIdAsync(int skillId);
        Task<IList<Skill>> GetSkillByIdsAsync(int[] skillIds);
        Task<IList<Skill>> GetAllSkillsAsync();
    }
}
