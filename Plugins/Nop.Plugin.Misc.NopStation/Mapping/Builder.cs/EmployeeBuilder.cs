using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Mapping.Builder
{
    public class EmployeeBuilder : NopEntityBuilder<Employee>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Employee.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(Employee.Name)).AsString(100).NotNullable()
                .WithColumn(nameof(Employee.EmployeeDesignationId)).AsInt32().NotNullable()
                .WithColumn(nameof(Employee.IsMVP)).AsBoolean().NotNullable()
                .WithColumn(nameof(Employee.IsNopCommerceCertified)).AsBoolean().NotNullable()
                .WithColumn(nameof(Employee.PictureId)).AsInt32().NotNullable()
                .WithColumn(nameof(Employee.EmployeeStatusId)).AsInt32().NotNullable();
        }
    }
}
