﻿using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStationTeam.Domain;

namespace Nop.Plugin.Misc.NopStationTeam.Mapping.Builders
{
    public class EmployeeBuilder : NopEntityBuilder<Employee>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Employee.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(Employee.Name)).AsString().NotNullable()
                .WithColumn(nameof(Employee.Designation)).AsString().NotNullable()
                .WithColumn(nameof(Employee.IsMVP)).AsBoolean().NotNullable()
                .WithColumn(nameof(Employee.IsNopCommerceCertified)).AsBoolean().NotNullable();
        }
    }
}
