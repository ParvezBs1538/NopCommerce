using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.NopStation.Areas.Admin.Factories;
using Nop.Plugin.Misc.NopStation.Services;
using Nop.Services.Common;
using NopStation.Plugin.Misc.Core.Infrastructure;

namespace Nop.Plugin.Misc.NopStation.Infrastructure;

public class PluginNopStartup : INopStartup
{
    public int Order => 3000;

    public void Configure(IApplicationBuilder application)
    {

    }
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new ViewLocationExpanderAdmin());
        });

        services.AddScoped<IPdfService, OverriddenPdfService>();

        services.AddScoped<IDeveloperService, DeveloperService>();
        services.AddScoped<IModifyOrderService, ModifyOrderService>();
        services.AddScoped<IDeveloperModelFactory, DeveloperModelFactory>();

        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<ISkillModelFactory, SkillModelFactory>();

        services.AddScoped<Factories.IDeveloperModelFactory, Factories.DeveloperModelFactory>();
        services.AddScoped<Factories.IModifyOrderModelFactory, Factories.ModifyOrderModelFactory>();

        services.AddNopStationServices("Nop.Plugin.Misc.NopStation");
    }
}