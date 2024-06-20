using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.BrainStation.Factories;
using Nop.Plugin.Misc.BrainStation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.BrainStation.Infrastructure
{
    public class NopStartup : INopStartup
    {
        public int Order => 2000;

        public void Configure(IApplicationBuilder application)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDeveloperDemoService, DeveloperDemoService>();
            services.AddScoped<IDeveloperDemoModelFactory, DeveloperDemoModelFactory>();    
        }
    }
}
