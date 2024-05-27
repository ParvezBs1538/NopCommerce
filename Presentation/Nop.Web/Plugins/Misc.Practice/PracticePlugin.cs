using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.WebApi.Frontend;

public class PracticePlugin : BasePlugin, IMiscPlugin
{
    #region Fields
    private readonly IWebHelper _webHelper;

    #endregion

    #region Ctor

    public PracticePlugin(IWebHelper webHelper)
    {
        _webHelper = webHelper;
    }

    #endregion

    #region Methods
    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/Practice/Configure";
    }
    public override async Task InstallAsync()
    {
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await base.UninstallAsync();
    }
    #endregion
}
