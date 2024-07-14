using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Builder
{
    public class EmployeeSkillBuilder : NopEntityBuilder<EmployeeSkillMapping>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(EmployeeSkillMapping.EmployeeId)).AsInt32().ForeignKey<Employee>()
                .WithColumn(nameof(EmployeeSkillMapping.SkillId)).AsInt32().ForeignKey<Skill>();
        }
    }
}
