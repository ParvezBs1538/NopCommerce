using Nop.Plugin.Misc.NopStation.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Areas.Admin.Factories
{
    public interface IDeveloperModelFactory
    {
        Task<DeveloperListModel> PrepareDeveloperListModelAsync(DeveloperSearchModel searchModel);

        Task<DeveloperSearchModel> PrepareDeveloperSearchModelAsync(DeveloperSearchModel searchModel);

        Task<DeveloperModel> PrepareDeveloperModelAsync(DeveloperModel model, Developer Developer, bool excludeProperties = false);
    }
}
