using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;

namespace Nop.Plugin.Misc.NopStation.Factories
{
    public interface ISkillModelFactory
    {
        Task<SkillListModel> PrepareSkillListModelAsync(SkillSearchModel searchModel);

        Task<SkillSearchModel> PrepareSkillSearchModelAsync(SkillSearchModel searchModel);

        Task<SkillModel> PrepareSkillModelAsync(SkillModel model, Skill Skill, bool excludeProperties = false);
    }
}
