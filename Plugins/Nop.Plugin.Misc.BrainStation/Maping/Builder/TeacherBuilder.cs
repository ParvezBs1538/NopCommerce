using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.BrainStation.Domain;

namespace Nop.Plugin.Misc.BrainStation.Maping.Builder
{
    public class TeacherBuilder : NopEntityBuilder<Teacher>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Teacher.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(Teacher.Name)).AsString(100).NotNullable()
                .WithColumn(nameof(Teacher.TeacherDesignationId)).AsInt32().NotNullable()
                .WithColumn(nameof(Teacher.IsCertified)).AsBoolean().NotNullable()
                .WithColumn(nameof(Teacher.PictureId)).AsInt32().NotNullable();
        }
    }
}
