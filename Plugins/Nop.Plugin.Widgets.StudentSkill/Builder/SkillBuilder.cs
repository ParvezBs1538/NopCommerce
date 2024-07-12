using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.StudentSkill.Domain;

namespace Nop.Plugin.Widgets.StudentSkill.Builder
{
    public class SkillBuilder : NopEntityBuilder<Skill>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Skill.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(Skill.Name)).AsString(100).NotNullable();
        }
    }
}
