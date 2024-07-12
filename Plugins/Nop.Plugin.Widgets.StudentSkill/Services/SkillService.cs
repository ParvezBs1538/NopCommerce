using Nop.Core;
using Nop.Data;
using Nop.Plugin.Widgets.StudentSkill.Domain;

namespace Nop.Plugin.Widgets.StudentSkill.Services
{
    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> _skillRepository;

        public SkillService(IRepository<Skill> skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task InsertSkillAsync(Skill skill)
        {
            await _skillRepository.InsertAsync(skill);
        }

        public async Task UpdateSkillAsync(Skill skill)
        {
            await _skillRepository.UpdateAsync(skill);
        }

        public async Task DeleteSkillAsync(Skill skill)
        {
            await _skillRepository.DeleteAsync(skill);
        }

        public async Task<IList<Skill>> GetAllSkillsAsync()
        {
            return await _skillRepository.Table.ToListAsync();
        }

        public async Task<Skill> GetSkillByIdAsync(int skillId)
        {
            return await _skillRepository.GetByIdAsync(skillId);
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
    }
}
