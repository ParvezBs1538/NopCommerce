using Nop.Plugin.Misc.NopStation.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Services.Localization;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Factories
{
    public class SkillModelFactory : ISkillModelFactory
    {
        private readonly ISkillService _SkillService;
        private readonly ILocalizationService _localizationService;

        public SkillModelFactory(ISkillService SkillService,
            ILocalizationService localizationService)
        {
            _SkillService = SkillService;
            _localizationService = localizationService;
        }
        public async Task<SkillListModel> PrepareSkillListModelAsync(SkillSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            var Skills = await _SkillService.SearchSkillsAsync(searchModel.Name,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            //await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            //{
            //    ["Admin.Misc.Skills"] = "Skills",
            //    ["Admin.Misc.Skills.AddNew"] = "Add new Skill",
            //    ["Admin.Misc.Skills.EditDetails"] = "Edit Skill details",

            //    ["Admin.Misc.Skills.BackToList"] = "back to Skill list",
            //    ["Admin.Misc.Skill.Fields.Name"] = "Name",
            //    ["Admin.Misc.Skill.Fields.Name.Hint"] = "Enter Skill name.",

            //    ["Admin.Misc.Skill.List.Name"] = "Name",
            //    ["Admin.Misc.Skill.List.Name.Hint"] = "Search by Skill name.",
            //});

            var model = await new SkillListModel().PrepareToGridAsync(searchModel, Skills, () =>
            {
                return Skills.SelectAwait(async Skill =>
                {
                    return await PrepareSkillModelAsync(null, Skill, true);
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
                    //fill in model values from the entity
                    model = new SkillModel()
                    {
                        Id = skill.Id,
                        Name = skill.Name,
                    };
                }
            }
            return model;
        }

        public async Task<SkillSearchModel> PrepareSkillSearchModelAsync(SkillSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }
    }
}
