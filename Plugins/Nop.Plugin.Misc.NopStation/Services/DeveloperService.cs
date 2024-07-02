using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IRepository<Developer> _developerRepository;

        public DeveloperService(IRepository<Developer> DeveloperRepository)
        {
            _developerRepository = DeveloperRepository;
        }

        public virtual async Task InsertDeveloperAsync(Developer Developer)
        {
            await _developerRepository.InsertAsync(Developer);
        }

        public virtual async Task UpdateDeveloperAsync(Developer Developer)
        {
            await _developerRepository.UpdateAsync(Developer);
        }

        public virtual async Task DeleteDeveloperAsync(Developer Developer)
        {
            await _developerRepository.DeleteAsync(Developer);
        }

        public virtual async Task<Developer> GetDeveloperByIdAsync(int DeveloperId)
        {
            return await _developerRepository.GetByIdAsync(DeveloperId);
        }

        public virtual async Task<IPagedList<Developer>> SearchDevelopersAsync(string name, int? statusId = null, int? designationId = null,
            bool? isMvp = null, bool? isCert = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _developerRepository.Table;
            //var query = from e in _developerRepository.Table
                        //select e;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            if (statusId > 0)
                query = query.Where(e => e.DeveloperStatusId == statusId);

            if (designationId > 0)
                query = query.Where(e => e.DeveloperDesignationId == designationId);

            //query = query.OrderBy(e => e.Name);
            query = query.OrderBy(e => e.DeveloperDesignationId)
                .ThenByDescending(e => e.IsMVP)
                .ThenByDescending(e => e.IsNopCommerceCertified)
                .ThenBy(e => e.Id);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
