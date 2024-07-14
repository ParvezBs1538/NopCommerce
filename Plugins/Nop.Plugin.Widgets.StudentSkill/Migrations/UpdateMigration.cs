using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.StudentSkill.Domain;

namespace Nop.Plugin.Widgets.StudentSkill.Migrations
{
    [NopSchemaMigration("2024/07/13 08:21:35:1687541", "Student Skill update migration", MigrationProcessType.Update)]
    public class UpdateMigration : Migration
    {
        public override void Up()
        {
            Create.TableFor<Student>();
        }
        public override void Down()
        {
        }
    }
}
