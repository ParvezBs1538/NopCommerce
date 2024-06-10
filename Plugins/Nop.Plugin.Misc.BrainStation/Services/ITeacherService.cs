using Nop.Core;
using Nop.Plugin.Misc.BrainStation.Domain;

namespace Nop.Plugin.Misc.BrainStation.Services
{
    public interface ITeacherService
    {
        Task InsertTeacherAsync(Teacher teacher);
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(Teacher teacher);
        Task GetTeacherByIdAsync(int teacher);
        Task<IPagedList<Teacher>> SearchTeachersAsync(string name, int DesignationId,
                int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
