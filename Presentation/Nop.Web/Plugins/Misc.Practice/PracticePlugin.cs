using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Misc.WebApi.Frontend;

public class PracticePlugin : BasePlugin, IMiscPlugin
{
    #region Fields

    //private readonly IPermissionService _permissionService;
    private readonly IWebHelper _webHelper;

    #endregion

    #region Ctor

    public PracticePlugin(IWebHelper webHelper)
    {
        //_permissionService = permissionService;
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
