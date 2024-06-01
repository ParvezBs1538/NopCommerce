using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.NopStation.Factories
{
    public class DeveloperModelFactory : IDeveloperModelFactory
    {
        private readonly IDeveloperService _DeveloperService;
        private readonly ILocalizationService _localizationService;

        public DeveloperModelFactory(IDeveloperService DeveloperService,
            ILocalizationService localizationService)
        {
            _DeveloperService = DeveloperService;
            _localizationService = localizationService;
        }

        public async Task<DeveloperListModel> PrepareDeveloperListModelAsync(DeveloperSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            var Developers = await _DeveloperService.SearchDevelopersAsync(searchModel.Name, searchModel.DeveloperStatusId,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new DeveloperListModel().PrepareToGridAsync(searchModel, Developers, () =>
            {
                return Developers.SelectAwait(async Developer =>
                {
                    return await PrepareDeveloperModelAsync(null, Developer, true);
                });
            });

            return model;
        }

        public async Task<DeveloperModel> PrepareDeveloperModelAsync(DeveloperModel model, Developer Developer, bool excludeProperties = false)
        {
            if (Developer != null)
            {
                if (model == null)
                {
                    //fill in model values from the entity
                    model = new DeveloperModel()
                    {
                        Designation = Developer.Designation,
                        DeveloperStatusId = Developer.DeveloperStatusId,
                        Id = Developer.Id,
                        IsMVP = Developer.IsMVP,
                        IsNopCommerceCertified = Developer.IsNopCommerceCertified,
                        Name = Developer.Name
                    };
                }
                model.DeveloperStatusStr = await _localizationService.GetLocalizedEnumAsync(Developer.DeveloperStatus);
            }

            if (!excludeProperties)
            {
                model.AvailableDeveloperStatusOptions = (await DeveloperStatus.Active.ToSelectListAsync()).ToList();
            }

            return model;
        }

        public async Task<DeveloperSearchModel> PrepareDeveloperSearchModelAsync(DeveloperSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            searchModel.AvailableDeveloperStatusOptions = (await DeveloperStatus.Active.ToSelectListAsync()).ToList();
            searchModel.AvailableDeveloperStatusOptions.Insert(0,
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }
    }
}
