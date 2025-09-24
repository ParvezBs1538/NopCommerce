using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Authentication.External;
using Nop.Services.Authentication.MultiFactor;
using Nop.Services.Catalog;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Payments;
using Nop.Services.Plugins;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Pickup;
using Nop.Services.Tax;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;
using NopStation.Plugin.Misc.Core.Models;

namespace NopStation.Plugin.Misc.Core.Factories;

public partial class PluginModelFactory : IPluginModelFactory
{
    #region Fields

    protected readonly IAclSupportedModelFactory _aclSupportedModelFactory;
    protected readonly IAuthenticationPluginManager _authenticationPluginManager;
    protected readonly IBaseAdminModelFactory _baseAdminModelFactory;
    protected readonly ILocalizationService _localizationService;
    protected readonly ILocalizedModelFactory _localizedModelFactory;
    protected readonly IMultiFactorAuthenticationPluginManager _multiFactorAuthenticationPluginManager;
    protected readonly IPaymentPluginManager _paymentPluginManager;
    protected readonly IPickupPluginManager _pickupPluginManager;
    protected readonly IPluginService _pluginService;
    protected readonly ISearchPluginManager _searchPluginManager;
    protected readonly IShippingPluginManager _shippingPluginManager;
    protected readonly IStaticCacheManager _staticCacheManager;
    protected readonly IStoreMappingSupportedModelFactory _storeMappingSupportedModelFactory;
    protected readonly ITaxPluginManager _taxPluginManager;
    protected readonly IWidgetPluginManager _widgetPluginManager;
    protected readonly IWorkContext _workContext;

    #endregion

    #region Ctor

    public PluginModelFactory(IAclSupportedModelFactory aclSupportedModelFactory,
        IAuthenticationPluginManager authenticationPluginManager,
        IBaseAdminModelFactory baseAdminModelFactory,
        ILocalizationService localizationService,
        IMultiFactorAuthenticationPluginManager multiFactorAuthenticationPluginManager,
        ILocalizedModelFactory localizedModelFactory,
        IPaymentPluginManager paymentPluginManager,
        IPickupPluginManager pickupPluginManager,
        IPluginService pluginService,
        ISearchPluginManager searchPluginManager,
        IShippingPluginManager shippingPluginManager,
        IStaticCacheManager staticCacheManager,
        IStoreMappingSupportedModelFactory storeMappingSupportedModelFactory,
        ITaxPluginManager taxPluginManager,
        IWidgetPluginManager widgetPluginManager,
        IWorkContext workContext)
    {
        _aclSupportedModelFactory = aclSupportedModelFactory;
        _authenticationPluginManager = authenticationPluginManager;
        _baseAdminModelFactory = baseAdminModelFactory;
        _localizationService = localizationService;
        _localizedModelFactory = localizedModelFactory;
        _multiFactorAuthenticationPluginManager = multiFactorAuthenticationPluginManager;
        _paymentPluginManager = paymentPluginManager;
        _pickupPluginManager = pickupPluginManager;
        _pluginService = pluginService;
        _searchPluginManager = searchPluginManager;
        _shippingPluginManager = shippingPluginManager;
        _staticCacheManager = staticCacheManager;
        _storeMappingSupportedModelFactory = storeMappingSupportedModelFactory;
        _taxPluginManager = taxPluginManager;
        _widgetPluginManager = widgetPluginManager;
        _workContext = workContext;
    }

    #endregion

    #region Utilities

    protected virtual IList<string> GetFilterableItems(IList<string> items, string excludedItem = "0")
    {
        if (items == null || !items.Any())
            return items;

        if (items.Contains(excludedItem))
            items.Remove(excludedItem);

        return items;
    }

    protected virtual void PrepareInstalledPluginModel(PluginModel model, IPlugin plugin)
    {
        ArgumentNullException.ThrowIfNull(model);

        ArgumentNullException.ThrowIfNull(plugin);

        //prepare configuration URL
        model.ConfigurationUrl = plugin.GetConfigurationPageUrl();

        //prepare enabled/disabled (only for some plugin types)
        model.CanChangeEnabled = true;
        switch (plugin)
        {
            case IMiscPlugin:
                model.CanChangeEnabled = false;
                break;

            case IPaymentMethod paymentMethod:
                model.IsEnabled = _paymentPluginManager.IsPluginActive(paymentMethod);
                break;

            case IShippingRateComputationMethod shippingRateComputationMethod:
                model.IsEnabled = _shippingPluginManager.IsPluginActive(shippingRateComputationMethod);
                break;

            case IPickupPointProvider pickupPointProvider:
                model.IsEnabled = _pickupPluginManager.IsPluginActive(pickupPointProvider);
                break;

            case ITaxProvider taxProvider:
                model.IsEnabled = _taxPluginManager.IsPluginActive(taxProvider);
                break;

            case IExternalAuthenticationMethod externalAuthenticationMethod:
                model.IsEnabled = _authenticationPluginManager.IsPluginActive(externalAuthenticationMethod);
                break;

            case IMultiFactorAuthenticationMethod multiFactorAuthenticationMethod:
                model.IsEnabled = _multiFactorAuthenticationPluginManager.IsPluginActive(multiFactorAuthenticationMethod);
                break;

            case ISearchProvider searchProvider:
                model.IsEnabled = _searchPluginManager.IsPluginActive(searchProvider);
                break;

            case IWidgetPlugin widgetPlugin:
                model.IsEnabled = _widgetPluginManager.IsPluginActive(widgetPlugin);
                break;

            default:
                model.CanChangeEnabled = false;
                break;
        }
    }

    #endregion

    #region Methods

    public virtual async Task<PluginSearchModel> PreparePluginSearchModelAsync(PluginSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(searchModel);

        //prepare available load plugin modes
        await _baseAdminModelFactory.PrepareLoadPluginModesAsync(searchModel.AvailableLoadModes, false);

        //prepare available groups
        await _baseAdminModelFactory.PreparePluginGroupsAsync(searchModel.AvailableGroups);

        //prepare available plugin authors
        var availablePluginGroups = (await _pluginService.GetPluginDescriptorsAsync<IPlugin>(LoadPluginsMode.All))
            .Select(plugin => plugin.Author).Distinct().OrderBy(authorName => authorName).ToList();
        foreach (var group in availablePluginGroups)
            searchModel.AvailableAuthors.Add(new SelectListItem { Value = @group, Text = @group });

        searchModel.AvailableAuthors.Add(new SelectListItem { Value = "0", Text = await _localizationService.GetResourceAsync("Admin.Common.All") });

        //prepare page parameters
        searchModel.SetGridPageSize();

        return searchModel;
    }

    public virtual async Task<PluginListModel> PreparePluginListModelAsync(PluginSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(searchModel);

        var loadMode = (LoadPluginsMode)searchModel.SearchLoadModeId;
        var friendlyName = string.IsNullOrEmpty(searchModel.SearchFriendlyName) ? null : searchModel.SearchFriendlyName;

        var plugins = (await _pluginService.GetPluginDescriptorsAsync<IPlugin>(loadMode: loadMode, friendlyName: friendlyName))
            .Where(p => p.ShowInPluginsList);

        var searchGroups = GetFilterableItems(searchModel.SearchGroups);
        if (searchGroups?.Any() ?? false)
            plugins = plugins.Where(p => searchGroups.Contains(p.Group));

        var searchAuthors = GetFilterableItems(searchModel.SearchAuthors);
        if (searchAuthors?.Any() ?? false)
            plugins = plugins.Where(p => searchAuthors.Contains(p.Author));

        var pagedPlugins = plugins.OrderBy(p => p.Group).ToList().ToPagedList(searchModel);

        //prepare list model
        var model = await new PluginListModel().PrepareToGridAsync(searchModel, pagedPlugins, () =>
        {
            return pagedPlugins.SelectAwait(async pluginDescriptor =>
            {
                //fill in model values from the entity
                var pluginModel = pluginDescriptor.ToPluginModel<PluginModel>();

                //fill in additional values (not existing in the entity)
                pluginModel.LogoUrl = await _pluginService.GetPluginLogoUrlAsync(pluginDescriptor);

                if (pluginDescriptor.Installed)
                    PrepareInstalledPluginModel(pluginModel, pluginDescriptor.Instance<IPlugin>());

                return pluginModel;
            });
        });

        return model;
    }

    public virtual async Task<PluginModel> PreparePluginModelAsync(PluginModel model, PluginDescriptor pluginDescriptor, bool excludeProperties = false)
    {
        Func<PluginLocalizedModel, int, Task> localizedModelConfiguration = null;

        if (pluginDescriptor != null)
        {
            //fill in model values from the entity
            model ??= pluginDescriptor.ToPluginModel(model);

            model.LogoUrl = await _pluginService.GetPluginLogoUrlAsync(pluginDescriptor);
            model.SelectedStoreIds = pluginDescriptor.LimitedToStores;
            model.SelectedCustomerRoleIds = pluginDescriptor.LimitedToCustomerRoles;
            var plugin = pluginDescriptor.Instance<IPlugin>();
            if (pluginDescriptor.Installed)
                PrepareInstalledPluginModel(model, plugin);

            //define localized model configuration action
            localizedModelConfiguration = async (locale, languageId) =>
            {
                locale.FriendlyName = await _localizationService.GetLocalizedFriendlyNameAsync(plugin, languageId, false);
            };
        }

        //prepare localized models
        if (!excludeProperties)
            model.Locales = await _localizedModelFactory.PrepareLocalizedModelsAsync(localizedModelConfiguration);

        //prepare model customer roles
        await _aclSupportedModelFactory.PrepareModelCustomerRolesAsync(model);

        //prepare available stores
        await _storeMappingSupportedModelFactory.PrepareModelStoresAsync(model);

        return model;
    }

    #endregion
}