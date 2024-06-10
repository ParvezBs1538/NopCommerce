using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.BrainStation.Domain;

namespace Nop.Plugin.Misc.BrainStation.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<Teacher> _teacherRepository;

        public TeacherService(IRepository<Teacher> teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public virtual async Task DeleteTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.DeleteAsync(teacher);
        }

        public virtual async Task GetTeacherByIdAsync(int teacher)
        {
            await _teacherRepository.GetByIdAsync(teacher);
        }

        public virtual async Task InsertTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.InsertAsync(teacher);
        }

        public virtual async Task UpdateTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.UpdateAsync(teacher);
        }

        public virtual async Task<IPagedList<Teacher>> SearchTeachersAsync(string name, int DesignationId,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from e in _teacherRepository.Table
                        select e;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            query = query.OrderBy(e => e.TeacherDesignationId);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
