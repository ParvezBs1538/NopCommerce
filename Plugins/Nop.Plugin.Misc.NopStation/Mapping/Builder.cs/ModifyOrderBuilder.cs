using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Mapping.Builder;

public class ModifyOrderBuilder : NopEntityBuilder<ModifyOrder>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(ModifyOrder.CustomerId)).AsInt32().NotNullable().ForeignKey<Customer>()
            .WithColumn(nameof(ModifyOrder.OrderId)).AsInt32().NotNullable().ForeignKey<Order>();
    }
}
