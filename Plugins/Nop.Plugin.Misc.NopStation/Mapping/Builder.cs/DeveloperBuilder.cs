using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Mapping.Builder
{
    public class DeveloperBuilder : NopEntityBuilder<Developer>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Developer.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(Developer.Name)).AsString(100).NotNullable()
                .WithColumn(nameof(Developer.DeveloperDesignationId)).AsInt32().NotNullable()
                .WithColumn(nameof(Developer.IsMVP)).AsBoolean().NotNullable()
                .WithColumn(nameof(Developer.IsNopCommerceCertified)).AsBoolean().NotNullable()
                .WithColumn(nameof(Developer.PictureId)).AsInt32().NotNullable()
                .WithColumn(nameof(Developer.DeveloperStatusId)).AsInt32().NotNullable();
        }
    }
}
