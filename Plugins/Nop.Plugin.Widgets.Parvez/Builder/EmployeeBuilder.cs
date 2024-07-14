using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Builder
{
    public class EmployeeBuilder : NopEntityBuilder<Employee>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Employee.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(Employee.Name)).AsString(100).NotNullable()
                .WithColumn(nameof(Employee.PictureId)).AsInt32().NotNullable()
                .WithColumn(nameof(Employee.StatusId)).AsInt32().NotNullable()
                .WithColumn(nameof(Employee.DesignationId)).AsInt32().NotNullable()
                .WithColumn(nameof(Employee.IsMVP)).AsBoolean().NotNullable()
                .WithColumn(nameof(Employee.IsCertified)).AsBoolean().NotNullable();
        }
    }
}
