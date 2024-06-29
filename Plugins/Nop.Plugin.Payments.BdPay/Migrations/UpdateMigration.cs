using FluentMigrator;
using Nop.Data.Migrations;

namespace Nop.Plugin.Payments.BdPay.Migrations
{
    [NopSchemaMigration("2024/06/27 06:24:35:1687541", "BdPay.Payment update migration", MigrationProcessType.Update)]
    public class UpdateMigration : Migration
    {
        public override void Up()
        {
            // Update string resource key values
            Execute.Sql("UPDATE LocaleStringResource SET ResourceValue = 'This payment method stores card information in database (it''s not sent to any third-party processor). In order to store card information, you must be PCI compliant.' WHERE ResourceName = 'Plugins.Payments.BdPay.Instructions'");
            Execute.Sql("UPDATE LocaleStringResource SET ResourceValue = 'Pay by BdPay card' WHERE ResourceName = 'Plugins.Payments.BdPay.PaymentMethodDescription'");
        }

        public override void Down()
        {
            // Revert string resource key values to previous state
            Execute.Sql("UPDATE LocaleStringResource SET ResourceValue = 'This payment method stores credit card information in database (it''s not sent to any third-party processor). In order to store credit card information, you must be PCI compliant.' WHERE ResourceName = 'Plugins.Payments.BdPay.Instructions'");
            Execute.Sql("UPDATE LocaleStringResource SET ResourceValue = 'Pay by credit / debit card' WHERE ResourceName = 'Plugins.Payments.BdPay.PaymentMethodDescription'");
        }
    }
}
