using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Plugin.Misc.NopStation.Components;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Misc.NopStation;

public class NopStationPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
{
    private readonly IWebHelper _webHelper;
    private readonly ILocalizationService _localizationService;

    public bool HideInWidgetList => false;

    public NopStationPlugin(IWebHelper webHelper,
        ILocalizationService localizationService)
    {
        _webHelper = webHelper;
        _localizationService = localizationService;
    }

    public Task ManageSiteMapAsync(SiteMapNode rootNode)
    {
        // Define the menu item for your NopStation plugin
        var menuItem = new SiteMapNode()
        {
            SystemName = "Misc.NopStation.Developer",
            Title = "Developer Management",
            ControllerName = "Developer",
            ActionName = "List",
            IconClass = "far fa-dot-circle",
            Visible = true,
            RouteValues = new RouteValueDictionary() { { "area", AreaNames.ADMIN } },
        };

        // Find the "NopStation" node in the root node's child nodes
        var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Misc");

        // If the "NopStation" node exists, add the custom menu item to its child nodes
        if (pluginNode != null)
        {
            pluginNode.ChildNodes.Add(menuItem);
        }
        // If the "NopStation" node doesn't exist, add the custom menu item to the root node's child nodes
        else
        {
            rootNode.ChildNodes.Add(menuItem);
        }

        return Task.CompletedTask;
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/Developer/List";
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
                //AdminWidgetZones.CategoryListButtons,
                //PublicWidgetZones.HomepageBottom,

                PublicWidgetZones.HomepageTop,
                PublicWidgetZones.CategoryDetailsTop,
            });
    }

    public Type GetWidgetViewComponent(string widgetZone)
    {
        if (widgetZone == PublicWidgetZones.HomepageTop)
        {
            return typeof(DeveloperViewComponent);
        }
        else
        {
            return typeof(CertifiedComponent);
        }
    }
}
