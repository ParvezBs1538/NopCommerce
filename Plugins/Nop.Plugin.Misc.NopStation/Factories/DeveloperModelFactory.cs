﻿using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.NopStation.Factories
{
    public class DeveloperModelFactory : IDeveloperModelFactory
    {
        #region Fields
        private readonly IDeveloperService _DeveloperService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        #endregion

        #region Ctor
        public DeveloperModelFactory(IDeveloperService DeveloperService,
            ILocalizationService localizationService,
            IPictureService pictureService)
        {
            _DeveloperService = DeveloperService;
            _localizationService = localizationService;
            _pictureService = pictureService;
        }
        #endregion

        #region PrepareDeveloperListModelAsync
        public async Task<DeveloperListModel> PrepareDeveloperListModelAsync(DeveloperSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(nameof(searchModel));

            var Developers = await _DeveloperService.SearchDevelopersAsync(searchModel.Name, searchModel.DeveloperStatusId,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Admin.Misc.Developers"] = "Developers",
                ["Admin.Misc.Developers.AddNew"] = "Add new Developer",
                ["Admin.Misc.Developers.EditDetails"] = "Edit Developer details",
                ["Admin.Misc.Developers.BackToList"] = "back to Developer list",

                ["Admin.Misc.Developer.Fields.Picture"] = "Picture",
                ["Admin.Misc.Developer.Fields.Picture.Hint"] = "Enter Picture.",
                ["Admin.Misc.Developer.Fields.Name"] = "Name",
                ["Admin.Misc.Developer.Fields.DeveloperDesignation"] = "Designation",
                ["Admin.Misc.Developer.Fields.IsMVP"] = "Is MVP",
                ["Admin.Misc.Developer.Fields.IsNopCommerceCertified"] = "Is certified",
                ["Admin.Misc.Developer.Fields.DeveloperStatus"] = "Status",
                ["Admin.Misc.Developer.Fields.Name.Hint"] = "Enter Developer name.",
                ["Admin.Misc.Developer.Fields.DeveloperDesignation.Hint"] = "Enter Developer designation.",
                ["Admin.Misc.Developer.Fields.IsMVP.Hint"] = "Check if Developer is MVP.",
                ["Admin.Misc.Developer.Fields.IsNopCommerceCertified.Hint"] = "Check if Developer is certified.",
                ["Admin.Misc.Developer.Fields.DeveloperStatus.Hint"] = "Select Developer status.",

                ["Admin.Misc.Developer.List.Name"] = "Name",
                ["Admin.Misc.Developer.List.DeveloperStatus"] = "Status",
                ["Admin.Misc.Developer.List.Name.Hint"] = "Search by Developer name.",
                ["Admin.Misc.Developer.List.DeveloperStatus.Hint"] = "Search by Developer status.",
                ["Admin.Misc.Developer.List.DeveloperDesignation"] = "Designation",
                ["Admin.Misc.Developer.List.DeveloperDesignation.Hint"] = "Search by Designation status.",

                ["Enums.Nop.Plugin.Misc.NopStation.Domain.DeveloperDesignation.HeadOfNopStation"] = "Head of nopStation",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.DeveloperDesignation.PrincipalEngineer"] = "Principal Engineer",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.DeveloperDesignation.ProjectManager"] = "Project Manager",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.DeveloperDesignation.LeadEngineer"] = "Lead Engineer",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.DeveloperDesignation.SeniorSoftwareEngineer"] = "Sr. Software Engineer",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.DeveloperDesignation.SoftwareEngineer"] = "Software Engineer",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.DeveloperDesignation.AssociateSoftwareEngineer"] = "Associate Software Engineer",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.DeveloperDesignation.Trainee"] = "Trainee"
            });

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
        #endregion

        #region PrepareDeveloperModelAsync
        public async Task<DeveloperModel> PrepareDeveloperModelAsync(DeveloperModel model, Developer developer, bool excludeProperties = false)
        {
            if (developer != null)
            {
                if (model == null)
                {
                    //fill in model values from the entity
                    model = new DeveloperModel()
                    {
                        DeveloperDesignationId = developer.DeveloperDesignationId,
                        DeveloperStatusId = developer.DeveloperStatusId,
                        Id = developer.Id,
                        IsMVP = developer.IsMVP,
                        IsNopCommerceCertified = developer.IsNopCommerceCertified,
                        Name = developer.Name,
                        PictureId = developer.PictureId
                    };
                }
                model.DeveloperStatusStr = await _localizationService.GetLocalizedEnumAsync(developer.DeveloperStatus);
                model.DeveloperDesignationStr = await _localizationService.GetLocalizedEnumAsync(developer.DeveloperDesignation);

                var picture = await _pictureService.GetPictureByIdAsync(developer.PictureId);
                (model.PictureThumbnailUrl, _) = await _pictureService.GetPictureUrlAsync(picture, 75);
            }

            if (!excludeProperties)
            {
                model.AvailableDeveloperStatusOptions = (await DeveloperStatus.Active.ToSelectListAsync()).ToList();
                model.AvailableDeveloperDesignationOptions = (await DeveloperDesignation.Trainee.ToSelectListAsync()).ToList();
            }

            return model;
        }
        #endregion

        #region PrepareDeveloperSearchModelAsync
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

            searchModel.AvailableDeveloperDesignationOptions = (await DeveloperDesignation.Trainee.ToSelectListAsync()).ToList();
            searchModel.AvailableDeveloperDesignationOptions.Insert(0,
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }
        #endregion
    }
}
