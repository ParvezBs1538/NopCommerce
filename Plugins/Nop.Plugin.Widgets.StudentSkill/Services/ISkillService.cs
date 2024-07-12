using Nop.Core;
using Nop.Plugin.Widgets.StudentSkill.Domain;

namespace Nop.Plugin.Widgets.StudentSkill.Services
{
    public interface ISkillService
    {
        Task InsertSkillAsync (Skill skill);
        Task UpdateSkillAsync (Skill skill);
        Task DeleteSkillAsync (Skill skill);
        Task<Skill> GetSkillByIdAsync (int skillId);
        Task<IList<Skill>> GetAllSkillsAsync ();
        Task<IPagedList<Skill>> SearchSkillsAsync(string name = null,
                int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
