using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Migrations
{
    [NopSchemaMigration("2024/07/14 10:00:35:1687541", "Nopstation.Developer update migration", MigrationProcessType.Update)]
    public class UpdateMigration : Migration
    {
        public override void Up()
        {
            //Create.TableFor<DeveloperSkillMapping>();
        }

        public override void Down()
        {
            //Delete.Table(nameof(DeveloperSkillMapping));
        }
    }
}
