using BioEngine.BRC.Core.Properties;
using BioEngine.BRC.Core.Publishing;
using BioEngine.BRC.IPB.Properties;
using BioEngine.BRC.IPB.Publishing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.IPB.Admin
{
    public class IPBAdminModule : IPBModule<IPBAdminModuleConfig>
    {
        public IPBAdminModule(IPBAdminModuleConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);

            services.AddScoped<IContentPublisher<IPBPublishConfig>, IPBContentPublisher>();
            services.AddScoped<IPBContentPublisher>();
            services.AddScoped<IPropertiesOptionsResolver, IPBSectionPropertiesOptionsResolver>();
        }
    }

    public class IPBAdminModuleConfig : IPBModuleConfig
    {
    }
}
