using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Services
{
    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> _skillRepository;
        private readonly IRepository<DeveloperSkillMapping> _developerSkillMappingRepository;

        public SkillService(IRepository<Skill> skillRepository,
            IRepository<DeveloperSkillMapping> developerSkillMappingRepository)
        {
            _skillRepository = skillRepository;
            _developerSkillMappingRepository = developerSkillMappingRepository;
        }

        public virtual async Task InsertSkillAsync(Skill Skill)
        {
            await _skillRepository.InsertAsync(Skill);
        }

        public virtual async Task UpdateSkillAsync(Skill Skill)
        {
            await _skillRepository.UpdateAsync(Skill);
        }

        public virtual async Task DeleteSkillAsync(Skill Skill)
        {
            await _skillRepository.DeleteAsync(Skill);
        }

        public virtual async Task<Skill> GetSkillByIdAsync(int skillId)
        {
            return await _skillRepository.GetByIdAsync(skillId);
        }
        
        public virtual async Task<IList<Skill>> GetSkillByIdsAsync(int[] skillIds)
        {
            return await _skillRepository.GetByIdsAsync(skillIds);
        }

        public async Task<IList<Skill>> GetAllSkillsAsync()
        {
            return await _skillRepository.Table.ToListAsync();
        }

        public virtual async Task<IPagedList<Skill>> SearchSkillsAsync(string name = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from e in _skillRepository.Table
                        select e;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        public virtual async Task DeleteDeveloperSkillMappingAsync(DeveloperSkillMapping developerSkillMapping)
        {
            await _developerSkillMappingRepository.DeleteAsync(developerSkillMapping);
        }

        public virtual async Task<IList<DeveloperSkillMapping>> GetDeveloperSkillMappingsByDeveloperIdAsync(int developerId)
        {
            return await _developerSkillMappingRepository.Table
                .Where(ds => ds.DeveloperId == developerId)
                .ToListAsync();
        }

        public virtual async Task<DeveloperSkillMapping> GetDeveloperSkillMappingByIdAsync(int developerSkillMappingId)
        {
            return await _developerSkillMappingRepository.GetByIdAsync(developerSkillMappingId, cache => default);
        }

        public virtual async Task InsertDeveloperSkillMappingAsync(DeveloperSkillMapping developerSkillMapping)
        {
            await _developerSkillMappingRepository.InsertAsync(developerSkillMapping);
        }

        public virtual async Task UpdateDeveloperSkillMappingAsync(DeveloperSkillMapping developerSkillMapping)
        {
            await _developerSkillMappingRepository.UpdateAsync(developerSkillMapping);
        }

        public virtual async Task<DeveloperSkillMapping> FindDeveloperSkillMappingAsync(int developerId, int skillId)
        {
            return await _developerSkillMappingRepository.Table
                .FirstOrDefaultAsync(ds => ds.SkillId == skillId && ds.DeveloperId == developerId);
        }
    }
}
