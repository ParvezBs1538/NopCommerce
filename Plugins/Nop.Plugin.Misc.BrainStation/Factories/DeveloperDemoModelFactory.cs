using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Misc.BrainStation.Domain;
using Nop.Plugin.Misc.BrainStation.Models;
using Nop.Plugin.Misc.BrainStation.Services;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.BrainStation.Factories
{
    public class DeveloperDemoModelFactory : IDeveloperDemoModelFactory
    {
        private readonly IDeveloperDemoService _developerDemoService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;

        public DeveloperDemoModelFactory(IDeveloperDemoService developerDemoService,
            ILocalizationService localizationService,
            IPictureService pictureService)
        {
            _developerDemoService = developerDemoService;
            _localizationService = localizationService;
            _pictureService = pictureService;
        }

        public async Task<DeveloperDemoListModel> PrepareDeveloperDemoListModelAsync(DeveloperDemoSearchModel searchModel)
        {
            var developers = await _developerDemoService.SearchDeveloperDemoAsync(searchModel.Name,
                searchModel.StatusId, searchModel.DesignationId, 
                pageIndex: searchModel.Page-1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new DeveloperDemoListModel().PrepareToGridAsync(searchModel, developers, () =>
            {
                return developers.SelectAwait(async developer =>
                {
                    return await PrepareDeveloperDemoModelAsync(null, developer, true);
                });
            });

            return model;
        }

        public async Task<DeveloperDemoModel> PrepareDeveloperDemoModelAsync(DeveloperDemoModel model, DeveloperDemo developerDemo, bool excludeProperties = false)
        {
            if (developerDemo != null)
            {
                if (model == null)
                {
                    //fill in model values from the entity
                    model = new DeveloperDemoModel()
                    {
                        Id = developerDemo.Id,
                        Name = developerDemo.Name,
                        DesignationId = developerDemo.DesignationId,
                        StatusId = developerDemo.StatusId,
                        IsMVP = developerDemo.IsMVP,
                        IsCertified = developerDemo.IsCertified,
                        PictureId = developerDemo.PictureId,
                    };
                }

                model.StatusStr = await _localizationService.GetLocalizedEnumAsync(developerDemo.Status);

                model.DesignationStr = await _localizationService.GetLocalizedEnumAsync(developerDemo.Designation);

                var picture = await _pictureService.GetPictureByIdAsync(developerDemo.PictureId);
                (model.PictureThumbnailUrl, _) = await _pictureService.GetPictureUrlAsync(picture, 75);
            }

            if (!excludeProperties)
            {
                model.AvailableStatusOptions = [.. await Status.Active.ToSelectListAsync()];

                model.AvailableDesignationOptions = [.. await Designation.Trainee.ToSelectListAsync()];
            }

            return model;
        }

        public async Task<DeveloperDemoSearchModel> PrepareDeveloperDemoSearchModelAsync(DeveloperDemoSearchModel searchModel)
        {
            searchModel.AvailableStatusOptions = (await Status.Active.ToSelectListAsync()).ToList();
            searchModel.AvailableStatusOptions.Insert(0,
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

            searchModel.AvailableDesignationOptions = (await Designation.Trainee.ToSelectListAsync()).ToList();
            searchModel.AvailableDesignationOptions.Insert(0,
                new SelectListItem
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
