using Nop.Plugin.Misc.BrainStation.Domain;
using Nop.Plugin.Misc.BrainStation.Models;

namespace Nop.Plugin.Misc.BrainStation.Factories
{
    public interface ITeacherModelFactory
    {
        Task<TeacherListModel> PrepareTeacherListModelAsync(TeacherSearchModel searchModel);
        Task<TeacherSearchModel> PrepareTeacherSearchModelAsync(TeacherSearchModel searchModel);
        Task<TeacherModel> PrepareTeacherModelAsync(TeacherModel model, Teacher teacher, bool excludeProperties = false);
    }
}
