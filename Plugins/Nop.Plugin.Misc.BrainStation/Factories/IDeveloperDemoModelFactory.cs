using Nop.Plugin.Misc.BrainStation.Domain;
using Nop.Plugin.Misc.BrainStation.Models;

namespace Nop.Plugin.Misc.BrainStation.Factories
{
    public interface IDeveloperDemoModelFactory
    {
        Task<DeveloperDemoListModel> PrepareDeveloperDemoListModelAsync(DeveloperDemoSearchModel searchModel);

        Task<DeveloperDemoSearchModel> PrepareDeveloperDemoSearchModelAsync(DeveloperDemoSearchModel searchModel);

        Task<DeveloperDemoModel> PrepareDeveloperDemoModelAsync(DeveloperDemoModel model, DeveloperDemo developerDemo, bool excludeProperties = false);
    }
}
