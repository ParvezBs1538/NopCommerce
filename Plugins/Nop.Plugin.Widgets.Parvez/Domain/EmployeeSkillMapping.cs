using Nop.Core;

namespace Nop.Plugin.Widgets.Parvez.Domain
{
    public class EmployeeSkillMapping : BaseEntity
    {
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
    }
}
