using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.Misc.NopStation.Services;

public partial class DeveloperReviewSendTask : IScheduleTask
{
    private readonly IDeveloperService _developerService;

    public DeveloperReviewSendTask(IDeveloperService developerService)
    {
        _developerService = developerService;
    }

    public async Task ExecuteAsync()
    {
        var developers = await _developerService.SearchDevelopersAsync();
        foreach (var developer in developers)
        {
            developer.DeveloperStatusId = (int)DeveloperStatus.Inactive;

            await _developerService.UpdateDeveloperAsync(developer);
        }
    }
}
