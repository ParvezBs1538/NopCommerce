using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.BrainStation
{
    public class BrainStationPlugin : BasePlugin, IMiscPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;

        public BrainStationPlugin(IWebHelper webHelper,
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _localizationService = localizationService;
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/Teacher/List";
        }

        public override async Task InstallAsync()
        {

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Admin.Misc.Teachers"] = "Teachers",
                ["Admin.Misc.Teachers.AddNew"] = "Add new Teacher",
                ["Admin.Misc.Teachers.EditDetails"] = "Edit Teacher details",
                ["Admin.Misc.Teachers.BackToList"] = "back to Teacher list",

                ["Admin.Misc.Teacher.Fields.Picture"] = "Picture",
                ["Admin.Misc.Teacher.Fields.Picture.Hint"] = "Enter Picture.",
                ["Admin.Misc.Teacher.Fields.Name"] = "Name",
                ["Admin.Misc.Teacher.Fields.TeacherDesignation"] = "Designation",
                ["Admin.Misc.Teacher.Fields.IsCertified"] = "Is certified",
                ["Admin.Misc.Teacher.Fields.Name.Hint"] = "Enter Teacher name.",
                ["Admin.Misc.Teacher.Fields.TeacherDesignation.Hint"] = "Enter Teacher designation.",
                ["Admin.Misc.Teacher.Fields.IsCertified.Hint"] = "Check if Teacher is certified.",

                ["Admin.Misc.Teacher.List.Name"] = "Name",
                ["Admin.Misc.Teacher.List.Name.Hint"] = "Search by Teacher name.",
                ["Admin.Misc.Teacher.List.TeacherDesignation"] = "Designation",
                ["Admin.Misc.Teacher.List.TeacherDesignation.Hint"] = "Search by Designation status.",

                ["Enums.Nop.Plugin.Misc.NopStation.Domain.TeacherDesignation.ViceChancellor"] = "Vice Chancellor",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.TeacherDesignation.ProViceChancellor"] = "Pro Vice Chancellor",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.TeacherDesignation.Dean"] = "Dean",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.TeacherDesignation.Chairman"] = "Chairman",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.TeacherDesignation.Professor"] = "Professor",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.TeacherDesignation.AssistantProfessor"] = "Assistant Professor",
                ["Enums.Nop.Plugin.Misc.NopStation.Domain.TeacherDesignation.Lecturer"] = "Lecturer"
            });

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }
    }
}