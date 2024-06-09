using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Mapping.Builder.cs
{
    public class DeveloperSkillBuilder : NopEntityBuilder<DeveloperSkillMapping>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(DeveloperSkillMapping.DeveloperId)).AsInt32().ForeignKey<Developer>()
                .WithColumn(nameof(DeveloperSkillMapping.SkillId)).AsInt32().ForeignKey<Skill>();
        }
    }
}
