using Nop.Plugin.Widgets.StudentSkill.Domain;
using Nop.Plugin.Widgets.StudentSkill.Models;
using Nop.Plugin.Widgets.StudentSkill.Services;
using Nop.Services.Localization;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Widgets.StudentSkill.Factories
{
    public class SkillModelFactory : ISkillModelFactory
    {
        private readonly ISkillService _skillService;
        private readonly ILocalizationService _localizationService;

        public SkillModelFactory(ISkillService skillService, ILocalizationService localizationService)
        {
            _skillService = skillService;
            _localizationService = localizationService;
        }

        public async Task<SkillListModel> PrepareSkillListModelAsync(SkillSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            var skills = await _skillService.SearchSkillsAsync(searchModel.Name,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            var model = await new SkillListModel().PrepareToGridAsync(searchModel, skills, () =>
            {
                return skills.SelectAwait(async developer =>
                {
                    return await PrepareSkillModelAsync(null, developer, true);
                });
            });

            return model;
        }

        public async Task<SkillModel> PrepareSkillModelAsync(SkillModel model, Skill skill, bool excludeProperties = false)
        {
            if (skill != null)
            {
                if (model == null)
                {
                    model = new SkillModel
                    {
                        Id = skill.Id,
                        Name = skill.Name
                    };
                }
            }
            return await Task.FromResult(model);
        }

        public async Task<SkillSearchModel> PrepareSkillSearchModelAsync(SkillSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return await Task.FromResult(searchModel);
        }
    }
}
