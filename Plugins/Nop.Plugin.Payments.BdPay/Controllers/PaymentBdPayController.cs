using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Plugin.Payments.BdPay.Models;
using Nop.Services;

namespace Nop.Plugin.Payments.BdPay.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class PaymentBdPayController : BasePaymentController
{
    protected readonly ILocalizationService _localizationService;
    protected readonly INotificationService _notificationService;
    protected readonly IPermissionService _permissionService;
    protected readonly ISettingService _settingService;
    protected readonly IStoreContext _storeContext;


    public PaymentBdPayController(ILocalizationService localizationService,
        INotificationService notificationService,
        IPermissionService permissionService,
        ISettingService settingService,
        IStoreContext storeContext)
    {
        _localizationService = localizationService;
        _notificationService = notificationService;
        _permissionService = permissionService;
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePaymentMethods))
            return AccessDeniedView();

        //load settings for a chosen store scope
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var bdPayPaymentSettings = await _settingService.LoadSettingAsync<BdPayPaymentSettings>(storeScope);

        var model = new ConfigurationModel
        {
            TransactModeId = Convert.ToInt32(bdPayPaymentSettings.TransactMode),
            AdditionalFee = bdPayPaymentSettings.AdditionalFee,
            AdditionalFeePercentage = bdPayPaymentSettings.AdditionalFeePercentage,
            TransactModeValues = await bdPayPaymentSettings.TransactMode.ToSelectListAsync(),
            ActiveStoreScopeConfiguration = storeScope
        };
        if (storeScope > 0)
        {
            model.TransactModeId_OverrideForStore = await _settingService.SettingExistsAsync(bdPayPaymentSettings, x => x.TransactMode, storeScope);
            model.AdditionalFee_OverrideForStore = await _settingService.SettingExistsAsync(bdPayPaymentSettings, x => x.AdditionalFee, storeScope);
            model.AdditionalFeePercentage_OverrideForStore = await _settingService.SettingExistsAsync(bdPayPaymentSettings, x => x.AdditionalFeePercentage, storeScope);
        }

        return View("~/Plugins/Payments.BdPay/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePaymentMethods))
            return AccessDeniedView();

        if (!ModelState.IsValid)
            return await Configure();

        //load settings for a chosen store scope
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var bdPayPaymentSettings = await _settingService.LoadSettingAsync<BdPayPaymentSettings>(storeScope);

        //save settings
        bdPayPaymentSettings.TransactMode = (TransactMode)model.TransactModeId;
        bdPayPaymentSettings.AdditionalFee = model.AdditionalFee;
        bdPayPaymentSettings.AdditionalFeePercentage = model.AdditionalFeePercentage;


        await _settingService.SaveSettingOverridablePerStoreAsync(bdPayPaymentSettings, x => x.TransactMode, model.TransactModeId_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(bdPayPaymentSettings, x => x.AdditionalFee, model.AdditionalFee_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(bdPayPaymentSettings, x => x.AdditionalFeePercentage, model.AdditionalFeePercentage_OverrideForStore, storeScope, false);

        //now clear settings cache
        await _settingService.ClearCacheAsync();

        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
