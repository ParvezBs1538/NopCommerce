using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.NopStationTeam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.NopStationTeam.Infrastructure;

public class NopStartup : INopStartup
{
    public int Order => 3000;

    public void Configure(IApplicationBuilder application)
    {

    }
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
    }
}