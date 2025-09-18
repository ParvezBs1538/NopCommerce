using FluentMigrator;
using Nop.Core.Domain.ScheduleTasks;
using Nop.Data;
using Nop.Data.Migrations;

namespace Nop.Plugin.Misc.NopStation.Migrations;

[NopSchemaMigration("2024-11-18 12:01:02", "NopStation.Team base data", MigrationProcessType.NoMatter)]
public class DataMigration : Migration
{
    protected readonly INopDataProvider _dataProvider;

    public DataMigration(INopDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }
    
    public override void Up()
    {
        var shipmentReviewTask = new ScheduleTask
        {
            Name = "Make developer inactive",
            Seconds = 86_400,
            Type = "Nop.Plugin.Misc.NopStation.Services.DeveloperReviewSendTask",
            Enabled = true,
            LastEnabledUtc = DateTime.UtcNow,
            StopOnError = false
        };

        if (!_dataProvider.GetTable<ScheduleTask>().Any(st => string.Compare(st.Name, shipmentReviewTask.Name, StringComparison.InvariantCultureIgnoreCase) == 0))
        {
            _dataProvider.InsertEntity(shipmentReviewTask);
        }
    }

    public override void Down()
    {
    }
}
