using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStation.Domain;

namespace Nop.Plugin.Misc.NopStation.Mapping.Builder
{
    public class DeveloperBuilder : NopEntityBuilder<Developer>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Developer.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(Developer.Name)).AsString(100).NotNullable()
                .WithColumn(nameof(Developer.DeveloperDesignationId)).AsInt32().NotNullable()
                .WithColumn(nameof(Developer.IsMVP)).AsBoolean().NotNullable()
                .WithColumn(nameof(Developer.IsNopCommerceCertified)).AsBoolean().NotNullable()
                .WithColumn(nameof(Developer.PictureId)).AsInt32().NotNullable()
                .WithColumn(nameof(Developer.DeveloperStatusId)).AsInt32().NotNullable();
        }
    }

    //public class SkillBuilder : NopEntityBuilder<Skill>
    //{
    //    public override void MapEntity(CreateTableExpressionBuilder table)
    //    {
    //        table.WithColumn(nameof(Skill.Id)).AsInt32().PrimaryKey().Identity()
    //            .WithColumn(nameof(Skill.Name)).AsString(100).NotNullable();
    //    }
    //}

    //public class DeveloperSkillBuilder : NopEntityBuilder<DeveloperSkill>
    //{
    //    public override void MapEntity(CreateTableExpressionBuilder table)
    //    {
    //        table.WithColumn(nameof(DeveloperSkill.Id)).AsInt32().PrimaryKey().Identity()
    //            .WithColumn(nameof(DeveloperSkill.DeveloperId)).AsInt32().NotNullable()
    //            .WithColumn(nameof(DeveloperSkill.SkillId)).AsInt32().NotNullable();
    //    }
    //}
}
