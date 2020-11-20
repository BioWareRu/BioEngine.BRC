using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Properties;
using BioEngine.BRC.Core.Publishing;
using BioEngine.BRC.Facebook.Entities;
using BioEngine.BRC.Facebook.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.Facebook
{
    public class FacebookModule : BaseApplicationModule
    {
        public FacebookModule(BaseApplicationModuleConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            services.AddSingleton<FacebookService>();
            services.AddScoped<IContentPublisher<FacebookConfig>, FacebookContentPublisher>();
            services.AddScoped<FacebookContentPublisher>();
            
            var registrar = BRCEntitiesRegistrar.Instance(services);
            registrar.RegisterSiteEntity<FacebookPublishRecord>();

            PropertiesProvider.RegisterBioEngineProperties<FacebookSitePropertiesSet, Site>("facebooksite");
        }
    }
}
