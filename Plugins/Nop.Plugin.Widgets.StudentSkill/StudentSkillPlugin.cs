using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.StudentSkill;

public class StudentSkillPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
{
    private readonly IWebHelper _webHelper;
    private readonly ILocalizationService _localizationService;

    public StudentSkillPlugin(IWebHelper webHelper, ILocalizationService localizationService)
    {
        _webHelper = webHelper;
        _localizationService = localizationService;
    }

    public bool HideInWidgetList => false;

    public Task ManageSiteMapAsync(SiteMapNode rootNode)
    {
        // Define the menu item for your Student Skill plugin
        var menuItem = new SiteMapNode()
        {
            SystemName = "Widgets.StuentSkill.Skill",
            Title = "Student Skill",
            ControllerName = "Student",
            ActionName = "List",
            IconClass = "far fa-dot-circle",
            Visible = true,
            RouteValues = new RouteValueDictionary() { { "area", AreaNames.ADMIN } },
        };

        // Find the "Student Skill" node in the root node's child nodes
        var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Widgets");

        // If the "Student Skill" node exists, add the custom menu item to its child nodes
        if (pluginNode != null)
        {
            pluginNode.ChildNodes.Add(menuItem);
        }
        // If the "Student Skill" node doesn't exist, add the custom menu item to the root node's child nodes
        else
        {
            rootNode.ChildNodes.Add(menuItem);
        }

        return Task.CompletedTask;
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/StudentSkill/List";
    }

    public override async Task InstallAsync()
    {
        await base.InstallAsync();
    }

    public override async Task UpdateAsync(string currentVersion, string targetVersion)
    {

        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Admin.Widgets.Skills"] = "Skills",
            ["Admin.Widgets.Skills.AddNew"] = "Add new Skill",
            ["Admin.Widgets.Skills.EditDetails"] = "Edit Skill details",
            ["Admin.Widgets.Skills.BackToList"] = "back to Skill list",

            ["Admin.Widgets.Skill.Fields.Name"] = "Name",
            ["Admin.Widgets.Skill.Fields.Name.Hint"] = "Enter Skill name.",

            ["Admin.Widgets.Skill.List.Name"] = "Name",
            ["Admin.Widgets.Skill.List.Name.Hint"] = "Search by Skill name.",


            ["Admin.Widgets.Students"] = "Students",
            ["Admin.Widgets.Students.AddNew"] = "Add new Student",
            ["Admin.Widgets.Students.EditDetails"] = "Edit Student details",
            ["Admin.Widgets.Students.BackToList"] = "back to Student list",

            ["Admin.Widgets.Student.Fields.Picture"] = "Picture",
            ["Admin.Widgets.Student.Fields.Picture.Hint"] = "Enter Student Picture",
            ["Admin.Widgets.Student.Fields.Name"] = "Name",
            ["Admin.Widgets.Student.Fields.Name.Hint"] = "Enter Student name",
            ["Admin.Widgets.Student.Fields.Status"] = "Status",
            ["Admin.Widgets.Student.Fields.Status.Hint"] = "Select Student status",
            ["Admin.Widgets.Student.Fields.Age"] = "Age",
            ["Admin.Widgets.Student.Fields.Age.Hint"] = "Select Student age",
            ["Admin.Widgets.Student.Fields.Skill"] = "Skill",
            ["Admin.Widgets.Student.Fields.Skill.Hint"] = "Select Student skill",


            ["Admin.Widgets.Student.List.Name"] = "Name",
            ["Admin.Widgets.Student.List.Name.Hint"] = "Search by Student name.",
            ["Admin.Widgets.Student.List.Status"] = "Status",
            ["Admin.Widgets.Student.List.Status.Hint"] = "Search by Student status.",
            ["Admin.Widgets.Student.List.Skill"] = "Skill",
            ["Admin.Widgets.Student.List.Skill.Hint"] = "Search by Student skill.",
        });

        await base.UpdateAsync(currentVersion, targetVersion);
    }
    public override async Task UninstallAsync()
    {
        await base.UninstallAsync();
    }

    public Type GetWidgetViewComponent(string widgetZone)
    {
        throw new NotImplementedException();
    }

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        throw new NotImplementedException();
    }
}
