using Nop.Core;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Services
{
    public interface IDeveloperService
    {
        Task InsertDeveloperAsync(Developer developer);

        Task UpdateDeveloperAsync(Developer developer);

        Task DeleteDeveloperAsync(Developer developer);

        Task<Developer> GetDeveloperByIdAsync(int developer);

        Task<IPagedList<Developer>> SearchDevelopersAsync(string name, int statusId,
                int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
