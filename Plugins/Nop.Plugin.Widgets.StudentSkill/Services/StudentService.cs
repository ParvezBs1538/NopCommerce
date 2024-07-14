using Nop.Core;
using Nop.Data;
using Nop.Plugin.Widgets.StudentSkill.Domain;

namespace Nop.Plugin.Widgets.StudentSkill.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task InsertStudentAsync(Student student)
        {
            await _studentRepository.InsertAsync(student);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(Student student)
        {
            await _studentRepository.DeleteAsync(student);
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return await _studentRepository.GetByIdAsync(studentId);
        }

        public async Task<IPagedList<Student>> SearchStudentsAsync(string name, int? statusId = 0, 
            int? skillId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _studentRepository.Table;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            if (statusId > 0)
                query = query.Where(e => e.StatusId == statusId);

            if (skillId > 0)
                query = query.Where(e => e.SkillId == skillId);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
