using DocumentFormat.OpenXml.Spreadsheet;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.NopStation
{
    public class NopStationPlugin : BasePlugin, IMiscPlugin
    {
        private readonly IWebHelper webHelper;
        private readonly ILocalizationService _localizationService;

        public NopStationPlugin(IWebHelper webHelper,
            ILocalizationService localizationService)
        {
            this.webHelper = webHelper;
            _localizationService = localizationService;
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{webHelper.GetStoreLocation()}Admin/Developer/List";
        }

        public override async Task InstallAsync()
        {
            //locales
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Admin.Misc.Developers"] = "Developers",
                ["Admin.Misc.Developers.AddNew"] = "Add new Developer",
                ["Admin.Misc.Developers.EditDetails"] = "Edit Developer details",
                ["Admin.Misc.Developers.BackToList"] = "back to Developer list",
                ["Admin.Misc.Developers"] = "Developers",
                ["Admin.Misc.Developers"] = "Developers",

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

        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }
    }
}
