using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Migrations
{
    [NopSchemaMigration("2024/06/01 08:20:55:1687541", "Nopstation.Developer base schema", MigrationProcessType.Installation)]

    public class SchemaMigration : Migration
    {
        public override void Up()
        {
            Create.TableFor<Developer>();
        }

        public override void Down()
        {
            Delete.Table(nameof(Developer));
        }
    }
}
