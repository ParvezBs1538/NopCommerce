using Nop.Plugin.Widgets.StudentSkill.Domain;
using Nop.Plugin.Widgets.StudentSkill.Models;

namespace Nop.Plugin.Widgets.StudentSkill.Factories
{
    public interface ISkillModelFactory
    {
        Task<SkillModel> PrepareSkillModelAsync(SkillModel model, Skill Skill, bool excludeProperties = false);
        Task<SkillListModel> PrepareSkillListModelAsync(SkillSearchModel searchModel);
        Task<SkillSearchModel> PrepareSkillSearchModelAsync(SkillSearchModel searchModel);
    }
}
