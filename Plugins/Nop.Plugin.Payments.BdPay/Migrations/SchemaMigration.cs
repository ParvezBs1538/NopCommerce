using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Payments.BdPay.Domain;

namespace Nop.Plugin.Payments.BdPay.Migrations
{
    [NopSchemaMigration("2024/06/21 02:45:35:1687541", "BdPay.Payment base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : Migration
    {
        public override void Up()
        {
            Create.TableFor<PaymentInfo>();
        }

        public override void Down()
        {
            Delete.Table(nameof(PaymentInfo));
        }
    }
}
