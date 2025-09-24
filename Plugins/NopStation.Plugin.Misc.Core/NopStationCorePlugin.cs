using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;
using NopStation.Plugin.Misc.Core.Components;
using NopStation.Plugin.Misc.Core.Services;

namespace NopStation.Plugin.Misc.Core;

public class NopStationCorePlugin : BasePlugin, IAdminMenuPlugin, INopStationPlugin, IWidgetPlugin
{
    private readonly IWebHelper _webHelper;
    private readonly INopStationCoreService _nopStationCoreService;
    private readonly ILocalizationService _localizationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly WidgetSettings _widgetSettings;

    public bool HideInWidgetList => true;

    public NopStationCorePlugin(IWebHelper webHelper,
        INopStationCoreService nopStationCoreService,
        ILocalizationService localizationService,
        IPermissionService permissionService,
        ISettingService settingService,
        WidgetSettings widgetSettings)
    {
        _webHelper = webHelper;
        _nopStationCoreService = nopStationCoreService;
        _localizationService = localizationService;
        _permissionService = permissionService;
        _settingService = settingService;
        _widgetSettings = widgetSettings;
    }

    public override string GetConfigurationPageUrl()
    {
        return _webHelper.GetStoreLocation() + "Admin/NopStationCore/Configure";
    }

    public override async Task InstallAsync()
    {
        var settings = new NopStationCoreSettings()
        {
            AllowedCustomerRoleIds = new List<int> { 1, 2 }
        };
        await _settingService.SaveSettingAsync(settings);

        await this.InstallPluginAsync(new CorePermissionProvider());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await this.UninstallPluginAsync(new CorePermissionProvider());
        await base.UninstallAsync();
    }

    public override async Task UpdateAsync(string currentVersion, string targetVersion)
    {
        if (targetVersion == "4.70.2.0")
        {
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains("NopStation.Core"))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add("NopStation.Core");
                await _settingService.SaveSettingAsync(_widgetSettings);
            }
        }

        await base.UpdateAsync(currentVersion, targetVersion);
    }

    public async Task ManageSiteMapAsync(SiteMapNode rootNode)
    {
        if (await _permissionService.AuthorizeAsync(CorePermissionProvider.ManageConfiguration))
        {
            var config = new SiteMapNode()
            {
                Title = await _localizationService.GetResourceAsync("Admin.NopStation.Core.Menu.Configuration"),
                Visible = true,
                IconClass = "far fa-circle",
                Url = "~/Admin/NopStationCore/Configure",
                SystemName = "NopStationCore.Configure"
            };
            await _nopStationCoreService.ManageSiteMapAsync(rootNode, config, NopStationMenuType.Core);

            var resource = new SiteMapNode()
            {
                Title = await _localizationService.GetResourceAsync("Admin.NopStation.Core.Menu.LocaleResources"),
                Visible = true,
                IconClass = "far fa-circle",
                Url = "~/Admin/NopStationCore/LocaleResource",
                SystemName = "NopStationCore.LocaleResources"
            };
            await _nopStationCoreService.ManageSiteMapAsync(rootNode, resource, NopStationMenuType.Core);

            var system = new SiteMapNode()
            {
                Title = await _localizationService.GetResourceAsync("Admin.NopStation.Core.Menu.AssemblyInfo"),
                Visible = true,
                IconClass = "fa fa-cog",
                Url = "~/Admin/NopStationCore/AssemblyInfo",
                SystemName = "NopStationCore.AssemblyInfo"
            };
            await _nopStationCoreService.ManageSiteMapAsync(rootNode, system, NopStationMenuType.Root);
        }

        if (await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageAcl))
        {
            var acl = new SiteMapNode()
            {
                Title = await _localizationService.GetResourceAsync("Admin.NopStation.Core.Menu.ACL"),
                Visible = true,
                IconClass = "far fa-circle",
                Url = "~/Admin/NopStationCore/Permissions",
                SystemName = "NopStationCore.ACL"
            };
            await _nopStationCoreService.ManageSiteMapAsync(rootNode, acl, NopStationMenuType.Core);
        }

        if (await _permissionService.AuthorizeAsync(CorePermissionProvider.ManageLicense))
        {
            var license = new SiteMapNode()
            {
                Title = await _localizationService.GetResourceAsync("Admin.NopStation.Core.Menu.License"),
                Visible = true,
                IconClass = "far fa-circle",
                Url = "~/Admin/NopStationLicense/License",
                SystemName = "NopStationCore.License"
            };
            await _nopStationCoreService.ManageSiteMapAsync(rootNode, license, NopStationMenuType.Core);
        }

        var reportBug = new SiteMapNode()
        {
            Title = await _localizationService.GetResourceAsync("Admin.NopStation.Core.Menu.ReportBug"),
            Visible = true,
            IconClass = "fa fa-bug",
            Url = "https://www.nop-station.com/report-bug?utm_source=admin-panel&utm_medium=products&utm_campaign=report-bug",
            OpenUrlInNewTab = true
        };
        await _nopStationCoreService.ManageSiteMapAsync(rootNode, reportBug, NopStationMenuType.Root);
    }

    public List<KeyValuePair<string, string>> PluginResouces()
    {
        var list = new List<KeyValuePair<string, string>>
        {
            new("Admin.NopStation.Core.AssemblyInfo", "Nop-Station assembly information"),
            new("Admin.NopStation.Core.Configuration", "Core settings"),
            new("Admin.NopStation.Core.LocaleResources", "String resources"),
            new("Admin.NopStation.Core.ACL", "Access control list"),
            new("Admin.NopStation.Core.License", "License"),
            new("Admin.NopStation.Core.Menu.NopStation", "Nop Station"),
            new("Admin.NopStation.Core.Menu.AssemblyInfo", "Assembly information"),
            new("Admin.NopStation.Core.Menu.Configuration", "Configuration"),
            new("Admin.NopStation.Core.Menu.LocaleResources", "String resources"),
            new("Admin.NopStation.Core.Menu.ACL", "Access control list"),
            new("Admin.NopStation.Core.Menu.License", "License"),
            new("Admin.NopStation.Core.Menu.Core", "Core settings"),
            new("Admin.NopStation.Core.Menu.Themes", "Themes"),
            new("Admin.NopStation.Core.Menu.Plugins", "Plugins"),
            new("Admin.NopStation.Core.Menu.ReportBug", "Report a bug"),
            new("Admin.NopStation.Core.License.InvalidProductKey", "Your product key is not valid."),
            new("Admin.NopStation.Core.License.InvalidForDomain", "Your product key is not valid for this domain."),
            new("Admin.NopStation.Core.License.InvalidForNOPVersion", "Your product key is not valid for this nopCommerce version."),
            new("Admin.NopStation.Core.License.Saved", "Your product key has been saved successfully."),
            new("Admin.NopStation.Core.License.LicenseString", "License string"),
            new("Admin.NopStation.Core.License.LicenseString.Hint", "Nop-station plugin/theme license string."),
            new("Admin.NopStation.Common.Menu.Documentation", "Documentation"),

            new("Admin.NopStation.Core.Resources.EditAccessDenied", "For security purposes, the feature you have requested is not available on this site."),
            new("Admin.NopStation.Core.Resources.FailedToSave", "Failed to save resource string."),
            new("Admin.NopStation.Core.Resources.Fields.Name", "Name"),
            new("Admin.NopStation.Core.Resources.Fields.Value", "Value"),
            new("Admin.NopStation.Core.Resources.List.SearchPluginSystemName", "Plugin"),
            new("Admin.NopStation.Core.Resources.List.SearchPluginSystemName.Hint", "Search resource string by plugin."),
            new("Admin.NopStation.Core.Resources.List.SearchResourceName", "Resource name"),
            new("Admin.NopStation.Core.Resources.List.SearchResourceName.Hint", "Search resource string by resource name."),
            new("Admin.NopStation.Core.Resources.List.SearchLanguageId", "Language"),
            new("Admin.NopStation.Core.Resources.List.SearchLanguageId.Hint", "Search resource string by language."),
            new("Admin.NopStation.Core.Resources.List.SearchPluginSystemName.All", "All"),

            new("Admin.NopStation.Core.Configuration.Fields.EnableCORS.ChangeHint", "Restart your application after changing this setting value."),
            new("Admin.NopStation.Core.Configuration.Fields.EnableCORS", "Enable CORS"),
            new("Admin.NopStation.Core.Configuration.Fields.EnableCORS.Hint", "Check to enable CORS. It will add \"Access-Control-Allow-Origin\" header for every api response."),
            new("Admin.NopStation.Core.Configuration.AdminCanNotBeRestricted", "Admin role can not be restricted."),
            new("Admin.NopStation.Core.Configuration.Fields.RestrictMainMenuByCustomerRoles", "Restrict main menu by customer roles"),
            new("Admin.NopStation.Core.Configuration.Fields.RestrictMainMenuByCustomerRoles.Hint", "Restrict main menu (Nop Station) by customer roles."),
            new("Admin.NopStation.Core.Configuration.Fields.AllowedCustomerRoles", "Allowed customer roles"),
            new("Admin.NopStation.Core.Configuration.Fields.AllowedCustomerRoles.Hint", "Select allowed customer roles to access Nop Station plugin menus. Make sure proper access provided for these customer roles from 'Access control list' page."),

            new("NopStation.Core.Request.Common.Ok", "Request success"),
            new("NopStation.Core.Request.Common.BadRequest", "Bad request"),
            new("NopStation.Core.Request.Common.Unauthorized", "Unauthorized"),
            new("NopStation.Core.Request.Common.NotFound", "Not found"),
            new("NopStation.Core.Request.Common.InternalServerError", "Internal server error"),

            new("Admin.NopStation.Core.SavePluginChanges", "Save changes")
        };

        return list;
    }

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new[]
        {
            AdminWidgetZones.PluginListButtons
        });
    }

    public Type GetWidgetViewComponent(string widgetZone)
    {
        return typeof(ManagePluginsViewComponent);
    }
}