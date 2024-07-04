using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Builder
{
    public class EmployeeBuilder : NopEntityBuilder<Employee>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(Employee.Name)).AsString(100)
                .WithColumn(nameof(Employee.PictureId)).AsInt32()
                .WithColumn(nameof(Employee.StatusId)).AsInt32()
                .WithColumn(nameof(Employee.DesignationId)).AsInt32()
                .WithColumn(nameof(Employee.IsMVP)).AsBoolean()
                .WithColumn(nameof(Employee.IsCertified)).AsBoolean();
        }
    }
}
