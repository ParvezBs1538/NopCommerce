using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Plugin.Misc.NopStation.Models;

namespace Nop.Plugin.Misc.NopStation.Factories;

public interface IDeveloperModelFactory 
{
    Task<IList<DeveloperModel>> PrepareDeveloperListModel(IList<Developer> developers);

    Task<DeveloperModel> PrepareDeveloperModel(Developer developer);
}
