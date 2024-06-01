using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.NopStation.Mapping.Builder.cs
{
    public class DeveloperBuilder : NopEntityBuilder<Developer>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Developer.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(Developer.Name)).AsString().NotNullable()
                .WithColumn(nameof(Developer.Designation)).AsString().NotNullable()
                .WithColumn(nameof(Developer.IsMVP)).AsBoolean().NotNullable()
                .WithColumn(nameof(Developer.IsNopCommerceCertified)).AsBoolean().NotNullable();
        }
    }
}
