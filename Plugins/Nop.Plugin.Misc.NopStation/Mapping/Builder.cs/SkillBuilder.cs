using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Mapping.Builder.cs
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
