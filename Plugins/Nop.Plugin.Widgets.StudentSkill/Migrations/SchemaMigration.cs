using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.StudentSkill.Domain;

namespace Nop.Plugin.Widgets.StudentSkill.Migrations
{
    [NopSchemaMigration("2024/07/13 12:10:35:1687541", "Student Skill base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : Migration
    {
        public override void Up()
        {
            Create.TableFor<Skill>();
        }
        public override void Down()
        {
        }
    }
}
