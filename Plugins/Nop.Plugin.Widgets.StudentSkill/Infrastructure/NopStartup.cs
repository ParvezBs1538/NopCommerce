using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.StudentSkill.Factories;
using Nop.Plugin.Widgets.StudentSkill.Services;

namespace Nop.Plugin.Widgets.StudentSkill.Infrastructure
{
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
                options.ViewLocationExpanders.Add(new ViewLocationExpanderStudentSkill());
            });

            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ISkillModelFactory, SkillModelFactory>();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentModelFactory, StudentModelFactory>();
        }
    }
}
