using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.BrainStation.Domain;

namespace Nop.Plugin.Misc.BrainStation.Services
{
    public class DeveloperDemoService : IDeveloperDemoService
    {
        private readonly IRepository<DeveloperDemo> _repository;

        public DeveloperDemoService(IRepository<DeveloperDemo> repository)
        {
            _repository = repository;
        }

        public virtual async Task DeleteDeveloperDemoAsync(DeveloperDemo developer)
        {
            await _repository.DeleteAsync(developer);
        }

        public virtual async Task<DeveloperDemo> GetDeveloperDemoByIdAsync(int developer)
        {
            return await _repository.GetByIdAsync(developer);
        }

        public virtual async Task InsertDeveloperDemoAsync(DeveloperDemo developer)
        {
            await _repository.InsertAsync(developer);
        }

        public virtual async Task<IPagedList<DeveloperDemo>> SearchDeveloperDemoAsync(string name, 
            int? statusId = null, int? designationId = null, 
            bool? isMvp = null, bool? isCert = null, 
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _repository.Table;
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));

            if (statusId > 0)
                query = query.Where(x => x.StatusId == statusId);

            if (designationId > 0)
                query = query.Where(x => x.DesignationId == designationId);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        public virtual async Task UpdateDeveloperDemoAsync(DeveloperDemo developer)
        {
            await _repository.UpdateAsync(developer);
        }
    }
}
