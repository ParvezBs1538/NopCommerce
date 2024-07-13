using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.NopStation.Areas.Admin.Factories;
using Nop.Plugin.Misc.NopStation.Services;

namespace Nop.Plugin.Misc.NopStation.Infrastructure;
public class NopStartup : INopStartup
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

        services.AddScoped<IDeveloperService, DeveloperService>();
        services.AddScoped<IDeveloperModelFactory, DeveloperModelFactory>();

        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<ISkillModelFactory, SkillModelFactory>();

        services.AddScoped<Factories.IDeveloperModelFactory, Factories.DeveloperModelFactory>();
    }
}