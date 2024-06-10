using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.BrainStation.Factories;
using Nop.Plugin.Misc.BrainStation.Services;

namespace Nop.Plugin.Misc.BrainStation.Infrastructure;

public class NopStartup : INopStartup
{
    public int Order => 3000;

    public void Configure(IApplicationBuilder application)
    {
        
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<ITeacherModelFactory, TeacherModelFactory>();
    }
}
