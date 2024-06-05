using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IRepository<Developer> _DeveloperRepository;

        public DeveloperService(IRepository<Developer> DeveloperRepository)
        {
            _DeveloperRepository = DeveloperRepository;
        }

        public virtual async Task InsertDeveloperAsync(Developer Developer)
        {
            await _DeveloperRepository.InsertAsync(Developer);
        }

        public virtual async Task UpdateDeveloperAsync(Developer Developer)
        {
            await _DeveloperRepository.UpdateAsync(Developer);
        }

        public virtual async Task DeleteDeveloperAsync(Developer Developer)
        {
            await _DeveloperRepository.DeleteAsync(Developer);
        }

        public virtual async Task<Developer> GetDeveloperByIdAsync(int DeveloperId)
        {
            return await _DeveloperRepository.GetByIdAsync(DeveloperId);
        }

        public virtual async Task<IPagedList<Developer>> SearchDevelopersAsync(string name, int statusId,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from e in _DeveloperRepository.Table
                        select e;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            if (statusId > 0)
                query = query.Where(e => e.DeveloperStatusId == statusId);

            //query = query.OrderBy(e => e.Name);
            query = query.OrderBy(e => e.DeveloperDesignationId);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
