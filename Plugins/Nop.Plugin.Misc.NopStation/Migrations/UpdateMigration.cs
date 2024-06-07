using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Migrations
{
    [NopSchemaMigration("2024/06/06 08:20:55:1687541", "Nopstation.Developer update migration", MigrationProcessType.Update)]
    public class UpdateMigration : Migration
    {
        public override void Up()
        {
            Create.TableFor<DeveloperSkillMapping>();
        }

        public override void Down()
        {
            Delete.Table(nameof(DeveloperSkillMapping));
        }
    }
}
