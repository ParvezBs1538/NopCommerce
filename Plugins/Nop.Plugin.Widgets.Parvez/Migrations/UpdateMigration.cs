using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Migrations
{
    [NopSchemaMigration("2024/07/14 05:10:55:1687541", "Parvez.Employee update migration", MigrationProcessType.Update)]
    public class UpdateMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            /*Create.TableFor<Employee>();
            Create.TableFor<Skill>();
            Create.TableFor<EmployeeSkillMapping>();*/
        }
    }
}
