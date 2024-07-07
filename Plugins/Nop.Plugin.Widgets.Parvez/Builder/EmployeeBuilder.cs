using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Builder
{
    public class EmployeeBuilder : NopEntityBuilder<BsEmployee>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(BsEmployee.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(BsEmployee.Name)).AsString(100).NotNullable()
                .WithColumn(nameof(BsEmployee.PictureId)).AsInt32().NotNullable()
                .WithColumn(nameof(BsEmployee.StatusId)).AsInt32().NotNullable()
                .WithColumn(nameof(BsEmployee.DesignationId)).AsInt32().NotNullable()
                .WithColumn(nameof(BsEmployee.IsMVP)).AsBoolean().NotNullable()
                .WithColumn(nameof(BsEmployee.IsCertified)).AsBoolean().NotNullable();
        }
    }
}
