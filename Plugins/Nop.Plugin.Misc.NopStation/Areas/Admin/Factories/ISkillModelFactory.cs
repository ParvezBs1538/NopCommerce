using Nop.Plugin.Misc.NopStation.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Factories
{
    public interface ISkillModelFactory
    {
        Task<SkillListModel> PrepareSkillListModelAsync(SkillSearchModel searchModel);

        Task<SkillSearchModel> PrepareSkillSearchModelAsync(SkillSearchModel searchModel);

        Task<SkillModel> PrepareSkillModelAsync(SkillModel model, Skill Skill, bool excludeProperties = false);
    }
}
