using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.BrainStation.Domain;

namespace Nop.Plugin.Misc.BrainStation.Maping.Builder
{
    public class DeveloperDemoBuilder : NopEntityBuilder<DeveloperDemo>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(DeveloperDemo.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(DeveloperDemo.Name)).AsString(100).NotNullable()
                .WithColumn(nameof(DeveloperDemo.DesignationId)).AsInt32().NotNullable()
                .WithColumn(nameof(DeveloperDemo.IsMVP)).AsBoolean().NotNullable()
                .WithColumn(nameof(DeveloperDemo.IsCertified)).AsBoolean().NotNullable()
                .WithColumn(nameof(DeveloperDemo.PictureId)).AsInt32().NotNullable()
                .WithColumn(nameof(DeveloperDemo.StatusId)).AsInt32().NotNullable();
        }
    }
}
