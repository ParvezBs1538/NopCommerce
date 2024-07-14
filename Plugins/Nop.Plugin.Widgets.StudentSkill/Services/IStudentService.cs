using Nop.Core;
using Nop.Plugin.Widgets.StudentSkill.Domain;

namespace Nop.Plugin.Widgets.StudentSkill.Services
{
    public interface IStudentService
    {
        Task InsertStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<IPagedList<Student>> SearchStudentsAsync(string name, int? StatusId = null, 
            int? SkillId = null, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
