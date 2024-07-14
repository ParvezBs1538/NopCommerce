using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.StudentSkill.Domain;

namespace Nop.Plugin.Widgets.StudentSkill.Builder
{
    public class StudentBuilder : NopEntityBuilder<Student>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Student.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(Student.Name)).AsString(100).NotNullable()
                .WithColumn(nameof(Student.Age)).AsInt32().NotNullable()
                .WithColumn(nameof(Student.StatusId)).AsInt32().NotNullable()
                .WithColumn(nameof(Student.PictureId)).AsInt32().NotNullable()
                .WithColumn(nameof(Student.SkillId)).AsInt32().NotNullable().ForeignKey<Skill>();
        }
    }
}
