using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.Ads.Web
{
    public class AdsSiteModule : AdsModule
    {
        public AdsSiteModule(AdsModuleConfig config, Application application) : base(config,
            application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            services.AddScoped<AdsProvider>();
        }
    }
}
