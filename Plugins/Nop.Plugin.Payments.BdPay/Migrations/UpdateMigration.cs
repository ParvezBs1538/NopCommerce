using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Payments.BdPay.Domain;

namespace Nop.Plugin.Payments.BdPay.Migrations
{
    [NopSchemaMigration("2024/06/30 12:43:35:1687541", "BdPay.Payment totalTk convert decimal", MigrationProcessType.Update)]
    public class UpdateMigration : Migration
    {
        public override void Up()
        {
            //add totaltk comumn
            /*Alter.Table(nameof(PaymentInfo))
                .AddColumn(nameof(PaymentInfo.TotalTk)).AsDecimal().NotNullable();*/

            // Convert totalTk to decimal
            Alter.Column(nameof(PaymentInfo.TotalTk)).OnTable(nameof(PaymentInfo)).AsDecimal().NotNullable();
		}

        public override void Down()
        {

        }
    }
}
