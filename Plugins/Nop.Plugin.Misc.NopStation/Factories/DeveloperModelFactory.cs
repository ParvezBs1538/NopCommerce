using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Models.Media;

namespace Nop.Plugin.Misc.NopStation.Factories;

public class DeveloperModelFactory : IDeveloperModelFactory
{
    private readonly ILocalizationService _localizationService;
    private readonly IPictureService _pictureService;
    private readonly ISkillService _skillService;

    public DeveloperModelFactory(ILocalizationService localizationService,
        IPictureService pictureService, ISkillService skillService
        )
    {
        _localizationService = localizationService;
        _pictureService = pictureService;
        _skillService = skillService;
    }

    public async Task<IList<DeveloperModel>> PrepareDeveloperListModel(IList<Developer> developers)
    {
        var model = new List<DeveloperModel>();
        foreach (var developer in developers )
        {
            model.Add(await PrepareDeveloperModel(developer));
        }
        return model;
    }

    public async Task<DeveloperModel> PrepareDeveloperModel(Developer developer)
    {
        var skillMappings = await _skillService.GetDeveloperSkillMappingsByDeveloperIdAsync(developer.Id);
        var skills = await _skillService.GetSkillByIdsAsync(skillMappings.Select(sm => sm.SkillId).ToArray());

        var picture = await _pictureService.GetPictureByIdAsync(developer.PictureId);

        var pictureModel = new PictureModel
        {
            AlternateText = "Picture of " + developer.Name,
            Title = "Picture of " + developer.Name,
            ThumbImageUrl = (await _pictureService.GetPictureUrlAsync(picture, 200)).Url,
            FullSizeImageUrl = (await _pictureService.GetPictureUrlAsync(picture)).Url,
            Id = developer.PictureId
        };

        return new DeveloperModel()
        {
            Id = developer.Id,
            Name = developer.Name,
            IsMVP = developer.IsMVP,
            IsNopCommerceCertified = developer.IsNopCommerceCertified,
            Skills = skills.Select(s => s.Name).ToArray(),
            Picture = pictureModel,
            DeveloperStatus = developer.DeveloperStatus,
            DeveloperStatusStr = await _localizationService.GetLocalizedEnumAsync(developer.DeveloperStatus),
            DeveloperDesignation = developer.DeveloperDesignation,
            DeveloperDesignationStr = await _localizationService.GetLocalizedEnumAsync(developer.DeveloperDesignation)
        };
    }
}
