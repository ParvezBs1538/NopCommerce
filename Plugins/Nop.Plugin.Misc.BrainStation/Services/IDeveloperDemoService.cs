using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.BrainStation.Domain;

namespace Nop.Plugin.Misc.BrainStation.Services
{
    public interface IDeveloperDemoService
    {
        Task InsertDeveloperDemoAsync(DeveloperDemo developer);

        Task UpdateDeveloperDemoAsync(DeveloperDemo developer);

        Task DeleteDeveloperDemoAsync(DeveloperDemo developer);

        Task<DeveloperDemo> GetDeveloperDemoByIdAsync(int developer);

        Task<IPagedList<DeveloperDemo>> SearchDeveloperDemoAsync(string name, int? statusId = null, int? designationId = null,
            bool? isMvp = null, bool? isCert = null, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
