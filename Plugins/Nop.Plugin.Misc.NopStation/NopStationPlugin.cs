using Nop.Core;
using Nop.Plugin.Misc.NopStation.Components;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Misc.NopStation;

public class NopStationPlugin : BasePlugin, IWidgetPlugin
{
    private readonly IWebHelper webHelper;
    private readonly ILocalizationService _localizationService;

    public bool HideInWidgetList => false;

    public NopStationPlugin(IWebHelper webHelper,
        ILocalizationService localizationService)
    {
        this.webHelper = webHelper;
        _localizationService = localizationService;
    }

    public override string GetConfigurationPageUrl()
    {
        //return $"{webHelper.GetStoreLocation()}Admin/Developer/List";
        return $"{webHelper.GetStoreLocation()}Admin/Developer/List";
    }

    public override async Task InstallAsync()
    {
        //locales
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {

            ["Admin.Misc.Skills"] = "Skills",
            ["Admin.Misc.Skills.AddNew"] = "Add new Skill",
            ["Admin.Misc.Skills.EditDetails"] = "Edit Skill details",
            ["Admin.Misc.Skills.BackToList"] = "back to Skill list",

            ["Admin.Misc.Skills.BackToList"] = "back to Skill list",
            ["Admin.Misc.Skill.Fields.Name"] = "Name",
            ["Admin.Misc.Skill.Fields.Name.Hint"] = "Enter Skill name.",

            ["Admin.Misc.Skill.List.Name"] = "Name",
            ["Admin.Misc.Skill.List.Name.Hint"] = "Search by Skill name.",



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

        await base.InstallAsync();
    }

    public override async Task UpdateAsync(string currentVersion, string targetVersion)
    {

        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {

            ["Admin.Misc.Skills"] = "Skills",
            ["Admin.Misc.Skills.AddNew"] = "Add new Skill",
            ["Admin.Misc.Skills.EditDetails"] = "Edit Skill details",
            ["Admin.Misc.Skills.BackToList"] = "back to Skill list",

            ["Admin.Misc.Skills.BackToList"] = "back to Skill list",
            ["Admin.Misc.Skill.Fields.Name"] = "Name",
            ["Admin.Misc.Skill.Fields.Name.Hint"] = "Enter Skill name.",

            ["Admin.Misc.Skill.List.Name"] = "Name",
            ["Admin.Misc.Skill.List.Name.Hint"] = "Search by Skill name.",



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

        await base.UpdateAsync(currentVersion, targetVersion);
    }

    public override async Task UninstallAsync()
    {
        await base.UninstallAsync();
    }

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(
            new List<string>
            {
            PublicWidgetZones.HomepageBottom
            });
    }


    public Type GetWidgetViewComponent(string widgetZone)
    {
        return typeof(DeveloperViewComponent);
    }
}
