using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.Parvez.Domain;

namespace Nop.Plugin.Widgets.Parvez.Migrations
{
    [NopSchemaMigration("2024/07/14 05:35:38:1687541", "NopStation.Employee base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : Migration
    {
        public override void Up()
        {
            //Create.TableFor<Employee>();
            //Create.TableFor<Skill>();
            //Create.TableFor<EmployeeSkillMapping>();
        }

        public override void Down()
        {
            //Delete.Table(nameof(Employee));
            //Delete.Table(nameof(Skill));
            //Delete.Table(nameof(EmployeeSkillMapping));
        }
    }
}
