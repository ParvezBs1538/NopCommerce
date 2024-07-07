using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Widgets.Parvez
{
    public class ParvezPlugin : BasePlugin, IMiscPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;

        public ParvezPlugin(IWebHelper webHelper, 
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _localizationService = localizationService;
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/BsEmployee/List";
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
    }
}
