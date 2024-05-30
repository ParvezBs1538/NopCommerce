using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.NopStationTeams;

public class NopStationTeamsPlugin : BasePlugin, IMiscPlugin
{
    private readonly IWebHelper webHelper;
    public NopStationTeamsPlugin(IWebHelper _webHelper)
    {
        webHelper = _webHelper;
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{webHelper.GetStoreLocation()}Admin/NopStationTeams/Configure";
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
