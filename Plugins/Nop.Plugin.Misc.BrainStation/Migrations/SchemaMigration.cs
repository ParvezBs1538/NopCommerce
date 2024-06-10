using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.BrainStation.Domain;

namespace Nop.Plugin.Misc.BrainStation.Migrations
{
    [NopSchemaMigration("2024/06/09 08:35:55:1687541", "BrainStation.Teacher base schema", MigrationProcessType.Installation)]

    public class SchemaMigration : Migration
    {
        public override void Up()
        {
            Create.TableFor<Teacher>();
        }

        public override void Down()
        {
            Delete.Table(nameof(Teacher));
        }
    }
}
