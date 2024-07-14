using Nop.Plugin.Widgets.StudentSkill.Domain;
using Nop.Plugin.Widgets.StudentSkill.Models;

namespace Nop.Plugin.Widgets.StudentSkill.Factories
{
    public interface IStudentModelFactory
    {
        Task<StudentModel> PrepareStudentModelAsync(StudentModel model, Student student, bool excludeProperties = false);
        Task<StudentListModel> PrepareStudentListModelAsync(StudentSearchModel searchModel);
        Task<StudentSearchModel> PrepareStudentSearchModelAsync(StudentSearchModel searchModel);
    }
}
