using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Nop.Services.Localization;
using NopStation.Plugin.Misc.Core.Services;

namespace NopStation.Plugin.Misc.Core.Infrastructure;

public class StartupEventHostedService : IHostedService
{
    private readonly ILanguageService _languageService;
    private readonly INopStationPluginManager _nopStationPluginManager;

    public StartupEventHostedService(ILanguageService languageService,
        INopStationPluginManager nopStationPluginManager)
    {
        _languageService = languageService;
        _nopStationPluginManager = nopStationPluginManager;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetAllLanguagesAsync();

        foreach (var language in languages)
            await _nopStationPluginManager.LoadPluginStringResourcesAsync(languageId: language.Id);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
