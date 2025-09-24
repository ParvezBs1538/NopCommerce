using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Migrations;

[NopSchemaMigration("2024/06/01 08:20:55:1788541", "NopStation.Developer base schema", MigrationProcessType.NoMatter)]

public class SchemaMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Skill))).Exists())
            Create.TableFor<Skill>();

        if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Developer))).Exists())
            Create.TableFor<Developer>();

        if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ModifyOrder))).Exists())
            Create.TableFor<ModifyOrder>();
    }

    public override void Down()
    {
        //Delete.Table(nameof(Developer));
        //Delete.Table(nameof(Skill));
        //Delete.Table(nameof(ModifyOrder));
    }
}
