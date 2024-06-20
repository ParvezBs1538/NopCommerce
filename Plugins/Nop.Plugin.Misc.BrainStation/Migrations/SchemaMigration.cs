using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.BrainStation.Domain;

namespace Nop.Plugin.Misc.BrainStation.Migrations
{
    [NopSchemaMigration("2024/06/18 15:38:35:1687541", "BrainStation.DeveloperDemo base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : Migration
    {
        public override void Up()
        {
            Create.TableFor<DeveloperDemo>();
        }

        public override void Down()
        {
            Delete.Table(nameof(DeveloperDemo));
        }
    }
}
