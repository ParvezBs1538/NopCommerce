using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.NopStationTeam
{
    public class NopStationTeamPlugin : BasePlugin, IMiscPlugin
    {
        private readonly IWebHelper webHelper;
        public NopStationTeamPlugin(IWebHelper webHelper)
        {
            this.webHelper = webHelper;
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{webHelper.GetStoreLocation()}Admin/NopStationTeam/Configure";
        }

        public override async Task InstallAsync()
        {
            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }
    }
}
