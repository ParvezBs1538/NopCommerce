using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Migrations
{
    [NopSchemaMigration("2024/03/05 15:38:35:353535", "Nopstation.Employee base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : Migration
    {
        public override void Up()
        {
            Create.TableFor<Employee>();
        }
        public override void Down()
        {
            Delete.Table(nameof(Employee));
        }
    }
}
