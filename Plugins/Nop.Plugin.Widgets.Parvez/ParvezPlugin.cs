using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Menu;
using Nop.Web.Framework;
using Nop.Services.Cms;
using Nop.Plugin.Widgets.Parvez.Components;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.Parvez
{
    public class ParvezPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;

        public bool HideInWidgetList => false;

        public ParvezPlugin(IWebHelper webHelper, 
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _localizationService = localizationService;
        }

        public Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            // Define the menu item for your Parvez plugin
            var menuItem = new SiteMapNode()
            {
                SystemName = "Widgets.Parvez.Employee",
                Title = "Employees Management",
                ControllerName = "Employee",
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
            return $"{_webHelper.GetStoreLocation()}Admin/Employee/List";
        }
        public override async Task InstallAsync()
        {
            await base.InstallAsync();
        }

        public override async Task UpdateAsync(string currentVersion, string targetVersion)
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                // Header Section
                ["Admin.Widgets.Employees"] = "Employees",
                ["Admin.Widgets.Employees.AddNew"] = "Add new Employee",
                ["Admin.Widgets.Employees.EditDetails"] = "Edit Employee details",
                ["Admin.Widgets.Employees.BackToList"] = "back to Employee list",

                //Create Section
                ["Admin.Widgets.Employee.Fields.Picture"] = "Picture",
                ["Admin.Widgets.Employee.Fields.Picture.Hint"] = "Enter Picture.",
                ["Admin.Widgets.Employee.Fields.Name"] = "Name",
                ["Admin.Widgets.Employee.Fields.Name.Hint"] = "Enter Employee name.",
                ["Admin.Widgets.Employee.Fields.Designation"] = "Designation",
                ["Admin.Widgets.Employee.Fields.Designation.Hint"] = "Enter Employee designation.",
                ["Admin.Widgets.Employee.Fields.MVP"] = "Is MVP",
                ["Admin.Widgets.Employee.Fields.MVP.Hint"] = "Check if Employee is MVP.",
                ["Admin.Widgets.Employee.Fields.Status"] = "Status",
                ["Admin.Widgets.Employee.Fields.Status.Hint"] = "Select Employee status.",
                ["Admin.Widgets.Employee.Fields.Certified"] = "Is certified",
                ["Admin.Widgets.Employee.Fields.Certified.Hint"] = "Check if Employee is certified.",
                
                //Search Section
                ["Admin.Widgets.Employee.List.Name"] = "Name",
                ["Admin.Widgets.Employee.List.Name.Hint"] = "Search by Employee name.",
                ["Admin.Widgets.Employee.List.Status"] = "Status",
                ["Admin.Widgets.Employee.List.Status.Hint"] = "Search by Employee status.",
                ["Admin.Widgets.Employee.List.Designation"] = "Designation",
                ["Admin.Widgets.Employee.List.Designation.Hint"] = "Search by Designation status.",

                // Enum Section
                ["Enums.Nop.Plugin.Widgets.Parvez.Domain.Designation.HeadOfParvez"] = "Head of Parvez",
                ["Enums.Nop.Plugin.Widgets.Parvez.Domain.Designation.PrincipalEngineer"] = "Principal Engineer",
                ["Enums.Nop.Plugin.Widgets.Parvez.Domain.Designation.ProjectManager"] = "Project Manager",
                ["Enums.Nop.Plugin.Widgets.Parvez.Domain.Designation.LeadEngineer"] = "Lead Engineer",
                ["Enums.Nop.Plugin.Widgets.Parvez.Domain.Designation.SeniorSoftwareEngineer"] = "Sr. Software Engineer",
                ["Enums.Nop.Plugin.Widgets.Parvez.Domain.Designation.SoftwareEngineer"] = "Software Engineer",
                ["Enums.Nop.Plugin.Widgets.Parvez.Domain.Designation.AssociateSoftwareEngineer"] = "Associate Software Engineer",
                ["Enums.Nop.Plugin.Widgets.Parvez.Domain.Designation.Trainee"] = "Trainee"
            });

            await base.UpdateAsync(currentVersion, targetVersion);
        }

        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>
                (new List<string> {
                    PublicWidgetZones.AccountNavigationAfter 
                    //PublicWidgetZones.HomepageTop,
                });
        }

        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(EmployeeViewComponent);
        }
    }
}
