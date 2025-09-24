using FluentMigrator.Builders.Alter;
using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Schema;
using FluentMigrator.Builders.Schema.Table;
using Nop.Core;
using Nop.Data.Mapping;

namespace NopStation.Plugin.Misc.Core.Extensions;

public static class FluentMigratorExtensions
{
    public static ISchemaTableSyntax Table<TEntity>(this ISchemaExpressionRoot schema) where TEntity : BaseEntity
    {
        return schema.Table(NameCompatibilityManager.GetTableName(typeof(TEntity)));
    }

    public static IAlterTableAddColumnOrAlterColumnOrSchemaOrDescriptionSyntax Table<TEntity>(this IAlterExpressionRoot alter) where TEntity : BaseEntity
    {
        return alter.Table(NameCompatibilityManager.GetTableName(typeof(TEntity)));
    }
}
