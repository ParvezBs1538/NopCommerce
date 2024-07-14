using Nop.Data.Mapping;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Builder
{
    internal class BaseNameCompatibility : INameCompatibility
    {
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(Employee), "BsEmployee" },
            { typeof(Skill), "BsEmployeeSkill" },
            { typeof(EmployeeSkillMapping), "BsEmployeeSkillMapping" }
        };

        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
        };
    }
}