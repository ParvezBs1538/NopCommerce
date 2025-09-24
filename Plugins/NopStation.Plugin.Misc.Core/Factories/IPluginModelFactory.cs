using System.Threading.Tasks;
using Nop.Services.Plugins;
using NopStation.Plugin.Misc.Core.Models;

namespace NopStation.Plugin.Misc.Core.Factories;

public partial interface IPluginModelFactory
{
    Task<PluginSearchModel> PreparePluginSearchModelAsync(PluginSearchModel searchModel);

    Task<PluginListModel> PreparePluginListModelAsync(PluginSearchModel searchModel);

    Task<PluginModel> PreparePluginModelAsync(PluginModel model, PluginDescriptor pluginDescriptor, bool excludeProperties = false);
}