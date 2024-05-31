using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.NopStationTeam.Domain;

namespace Nop.Plugin.Misc.NopStationTeam.Migrations
{
    [NopSchemaMigration("2024/05/30 08:20:55:1687541", "NopstationTeam.Employee base schema", MigrationProcessType.Installation)]

    public class SchemaMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.TableFor<Employee>();
        }
    }
}
