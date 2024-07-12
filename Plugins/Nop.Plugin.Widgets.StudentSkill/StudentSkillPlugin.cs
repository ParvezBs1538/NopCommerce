using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.StudentSkill;

public class StudentSkillPlugin : BasePlugin, IWidgetPlugin
{
    private readonly IWebHelper _webHelper;

    public StudentSkillPlugin(IWebHelper webHelper)
    {
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;

    public Task ManageSiteMapAsync(SiteMapNode rootNode)
    {
        // Define the menu item for your Student Skill plugin
        var menuItem = new SiteMapNode()
        {
            SystemName = "Widgets.StuentSkill.Student",
            Title = "Student Skill",
            ControllerName = "Skill",
            ActionName = "List",
            IconClass = "far fa-dot-circle",
            Visible = true,
            RouteValues = new RouteValueDictionary() { { "area", AreaNames.ADMIN } },
        };

        // Find the "NopStation" node in the root node's child nodes
        var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Widgets");

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
        return $"{_webHelper.GetStoreLocation()}Admin/Skill/List";
    }
    public override async Task InstallAsync()
    {
        await base.InstallAsync();
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
