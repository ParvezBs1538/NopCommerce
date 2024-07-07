using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Migrations
{
    [NopSchemaMigration("2024/07/06 15:38:35:1687541", "Nopstation.Employee base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : Migration
    {
        public override void Up()
        {
            Create.TableFor<BsEmployee>();
        }
        public override void Down()
        {
            Delete.Table(nameof(BsEmployee));
        }
    }
}
