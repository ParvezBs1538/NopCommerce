using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;

namespace Nop.Plugin.Misc.NopStation.Factories;

public interface IDeveloperModelFactory 
{
    Task<IList<DeveloperModel>> PrepareDeveloperListModelAsync(IList<Developer> developers, string widgetZone);

    Task<DeveloperModel> PrepareDeveloperModelAsync(Developer developer);
}
