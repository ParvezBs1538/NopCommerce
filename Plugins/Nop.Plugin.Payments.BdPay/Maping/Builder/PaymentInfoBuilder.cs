﻿using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Payments.BdPay.Domain;


namespace Nop.Plugin.Payments.BdPay.Maping.Builder
{
    public class PaymentInfoBuilder : NopEntityBuilder<PaymentInfo>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(PaymentInfo.AccountType)).AsString(100).NotNullable()
                .WithColumn(nameof(PaymentInfo.MobileNumber)).AsString(100).NotNullable()
                .WithColumn(nameof(PaymentInfo.TransactionId)).AsString(100).NotNullable();
        }
    }
}