using Nop.Data;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Services
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

        public async Task<IList<Skill>> GetSkillByIdsAsync(int[] skillIds)
        {
            return await _skillRepository.GetByIdsAsync(skillIds);
        }
    }
}
