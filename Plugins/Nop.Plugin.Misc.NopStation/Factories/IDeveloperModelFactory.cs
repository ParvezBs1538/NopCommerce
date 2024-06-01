using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;

namespace Nop.Plugin.Misc.NopStation.Factories
{
    public interface IDeveloperModelFactory
    {
        Task<DeveloperListModel> PrepareDeveloperListModelAsync(DeveloperSearchModel searchModel);

        Task<DeveloperSearchModel> PrepareDeveloperSearchModelAsync(DeveloperSearchModel searchModel);

        Task<DeveloperModel> PrepareDeveloperModelAsync(DeveloperModel model, Developer Developer, bool excludeProperties = false);
    }
}
