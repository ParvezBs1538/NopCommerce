using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Media;

namespace Nop.Plugin.Misc.NopStation.Factories;

public class DeveloperModelFactory : IDeveloperModelFactory
{
    #region Fields

    private readonly ILocalizationService _localizationService;
    private readonly IPictureService _pictureService;
    private readonly ISkillService _skillService;

    #endregion

    #region Ctor

    public DeveloperModelFactory(ILocalizationService localizationService,
        IPictureService pictureService, ISkillService skillService
        )
    {
        _localizationService = localizationService;
        _pictureService = pictureService;
        _skillService = skillService;
    }

    #endregion


    #region Methods

    public async Task<IList<DeveloperModel>> PrepareDeveloperListModelAsync(IList<Developer> developers, string widgetZone, bool isok)
    {
        int count = 0;
        var model = new List<DeveloperModel>();

        foreach (var developer in developers )
        {
            count++;
            if (widgetZone == PublicWidgetZones.CategoryDetailsTop && count % 2 == 0)
            {
                if (isok == true)
                {
                    model.Add(await PrepareDeveloperModelAsync(developer));
                }
                continue;
            }
            if (isok == false) model.Add(await PrepareDeveloperModelAsync(developer));
        }
        return model;
    }

    public async Task<DeveloperModel> PrepareDeveloperModelAsync(Developer developer)
    {
        var skillMappings = await _skillService.GetDeveloperSkillMappingsByDeveloperIdAsync(developer.Id);
        var developerSkills = await _skillService.GetSkillByIdsAsync(skillMappings.Select(sm => sm.SkillId).ToArray());

        var picture = await _pictureService.GetPictureByIdAsync(developer.PictureId);

        var pictureModel = new PictureModel
        {
            Id = developer.PictureId,
            AlternateText = "Picture of " + developer.Name,
            Title = "Picture of " + developer.Name,
            ThumbImageUrl = (await _pictureService.GetPictureUrlAsync(picture, 200)).Url,
            FullSizeImageUrl = (await _pictureService.GetPictureUrlAsync(picture)).Url,
        };

        return new DeveloperModel()
        {
            Id = developer.Id,
            Name = developer.Name,
            IsMVP = developer.IsMVP,
            IsNopCommerceCertified = developer.IsNopCommerceCertified,
            Skills = developerSkills.Select(s => s.Name).ToArray(),
            Picture = pictureModel,
            DeveloperStatus = developer.DeveloperStatus,
            DeveloperStatusStr = await _localizationService.GetLocalizedEnumAsync(developer.DeveloperStatus),
            DeveloperDesignation = developer.DeveloperDesignation,
            DeveloperDesignationStr = await _localizationService.GetLocalizedEnumAsync(developer.DeveloperDesignation)
        };
    }

    #endregion
}
