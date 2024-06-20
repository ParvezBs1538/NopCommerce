using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.ShopManagement;

public class ShopManagementPlugin : BasePlugin, IMiscPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly IWebHelper _webHelper;

    public ShopManagementPlugin(IWebHelper webHelper, ILocalizationService localizationService)
    {
        _webHelper = webHelper;
        _localizationService = localizationService;
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/Product/List";
    }

    public override async Task InstallAsync()
    {
        //await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        //{
            
        //});

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await base.UninstallAsync();
    }
}
