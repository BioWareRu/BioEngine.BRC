using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Properties;
using BioEngine.BRC.Core.Publishing;
using BioEngine.BRC.Twitter.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.Twitter
{
    public class TwitterModule : BaseApplicationModule
    {
        public TwitterModule(BaseApplicationModuleConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            services.AddSingleton<TwitterService>();
            services.AddScoped<IContentPublisher<TwitterPublishConfig>, TwitterContentPublisher>();
            services.AddScoped<TwitterContentPublisher>();
            
            var registrar = BRCEntitiesRegistrar.Instance(services);
            registrar.RegisterSiteEntity<TwitterPublishRecord>();

            PropertiesProvider.RegisterBioEngineProperties<TwitterSitePropertiesSet, Site>("twittersite");
        }
    }
}
