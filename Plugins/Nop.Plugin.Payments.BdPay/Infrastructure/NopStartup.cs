using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Payments.BdPay.Services;

namespace Nop.Plugin.Payments.BdPay.Infrastructure;

public class NopStartup : INopStartup
{
    public int Order => 1000;

    public void Configure(IApplicationBuilder application)
    {
        
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPaymentInfoServices, PaymentInfoServices>();
    }
}
