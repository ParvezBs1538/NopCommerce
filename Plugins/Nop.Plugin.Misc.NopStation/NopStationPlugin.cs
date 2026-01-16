using Nop.Core;
using Nop.Plugin.Misc.NopStation.Components;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Misc.NopStation;

public class NopStationPlugin : BasePlugin, IWidgetPlugin
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

            ["Admin.Misc.Employees"] = "Employees",
            ["Admin.Misc.Employees.AddNew"] = "Add new Employee",
            ["Admin.Misc.Employees.EditDetails"] = "Edit Employee details",
            ["Admin.Misc.Employees.BackToList"] = "back to Employee list",

            ["Admin.Misc.Employee.Fields.Picture"] = "Picture",
            ["Admin.Misc.Employee.Fields.Picture.Hint"] = "Enter Picture.",
            ["Admin.Misc.Employee.Fields.Name"] = "Name",
            ["Admin.Misc.Employee.Fields.Name.Required"] = "Employee name is required.",
            ["Admin.Misc.Employee.Fields.EmployeeDesignation"] = "Designation",
            ["Admin.Misc.Employee.Fields.Designation.Required"] = "Employee designation is required.",
            ["Admin.Misc.Employee.Fields.IsMVP"] = "Is MVP",
            ["Admin.Misc.Employee.Fields.IsNopCommerceCertified"] = "Is certified",
            ["Admin.Misc.Employee.Fields.EmployeeStatus"] = "Status",
            ["Admin.Misc.Employee.Fields.Name.Hint"] = "Enter Employee name.",
            ["Admin.Misc.Employee.Fields.EmployeeDesignation.Hint"] = "Enter Employee designation.",
            ["Admin.Misc.Employee.Fields.IsMVP.Hint"] = "Check if Employee is MVP.",
            ["Admin.Misc.Employee.Fields.IsNopCommerceCertified.Hint"] = "Check if Employee is certified.",
            ["Admin.Misc.Employee.Fields.EmployeeStatus.Hint"] = "Select Employee status.",

            ["Admin.Misc.Employee.List.Name"] = "Name",
            ["Admin.Misc.Employee.List.EmployeeStatus"] = "Status",
            ["Admin.Misc.Employee.List.Name.Hint"] = "Search by Employee name.",
            ["Admin.Misc.Employee.List.EmployeeStatus.Hint"] = "Search by Employee status.",
            ["Admin.Misc.Employee.List.EmployeeDesignation"] = "Designation",
            ["Admin.Misc.Employee.List.EmployeeDesignation.Hint"] = "Search by Designation status.",

            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.HeadOfNopStation"] = "Head of nopStation",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.PrincipalEngineer"] = "Principal Engineer",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.ProjectManager"] = "Project Manager",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.LeadEngineer"] = "Lead Engineer",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.SeniorSoftwareEngineer"] = "Sr. Software Engineer",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.SoftwareEngineer"] = "Software Engineer",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.AssociateSoftwareEngineer"] = "Associate Software Engineer",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.Trainee"] = "Trainee",



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

            ["Admin.Misc.Employees"] = "Employees",
            ["Admin.Misc.Employees.AddNew"] = "Add new Employee",
            ["Admin.Misc.Employees.EditDetails"] = "Edit Employee details",
            ["Admin.Misc.Employees.BackToList"] = "back to Employee list",

            ["Admin.Misc.Employee.Fields.Picture"] = "Picture",
            ["Admin.Misc.Employee.Fields.Picture.Hint"] = "Enter Picture.",
            ["Admin.Misc.Employee.Fields.Name"] = "Name",
            ["Admin.Misc.Employee.Fields.Name.Required"] = "Employee name is required.",
            ["Admin.Misc.Employee.Fields.EmployeeDesignation"] = "Designation",
            ["Admin.Misc.Employee.Fields.Designation.Required"] = "Employee designation is required.",
            ["Admin.Misc.Employee.Fields.IsMVP"] = "Is MVP",
            ["Admin.Misc.Employee.Fields.IsNopCommerceCertified"] = "Is certified",
            ["Admin.Misc.Employee.Fields.EmployeeStatus"] = "Status",
            ["Admin.Misc.Employee.Fields.Name.Hint"] = "Enter Employee name.",
            ["Admin.Misc.Employee.Fields.EmployeeDesignation.Hint"] = "Enter Employee designation.",
            ["Admin.Misc.Employee.Fields.IsMVP.Hint"] = "Check if Employee is MVP.",
            ["Admin.Misc.Employee.Fields.IsNopCommerceCertified.Hint"] = "Check if Employee is certified.",
            ["Admin.Misc.Employee.Fields.EmployeeStatus.Hint"] = "Select Employee status.",

            ["Admin.Misc.Employee.List.Name"] = "Name",
            ["Admin.Misc.Employee.List.EmployeeStatus"] = "Status",
            ["Admin.Misc.Employee.List.Name.Hint"] = "Search by Employee name.",
            ["Admin.Misc.Employee.List.EmployeeStatus.Hint"] = "Search by Employee status.",
            ["Admin.Misc.Employee.List.EmployeeDesignation"] = "Designation",
            ["Admin.Misc.Employee.List.EmployeeDesignation.Hint"] = "Search by Designation status.",

            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.HeadOfNopStation"] = "Head of nopStation",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.PrincipalEngineer"] = "Principal Engineer",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.ProjectManager"] = "Project Manager",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.LeadEngineer"] = "Lead Engineer",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.SeniorSoftwareEngineer"] = "Sr. Software Engineer",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.SoftwareEngineer"] = "Software Engineer",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.AssociateSoftwareEngineer"] = "Associate Software Engineer",
            ["Enums.Nop.Plugin.Misc.NopStation.Domain.EmployeeDesignation.Trainee"] = "Trainee",



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
