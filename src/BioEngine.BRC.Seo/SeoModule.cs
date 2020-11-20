using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.Seo
{
    public class SeoModule : BaseApplicationModule
    {
        public SeoModule(BaseApplicationModuleConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            PropertiesProvider.RegisterBioEngineSectionProperties<SeoContentPropertiesSet>("seo");
            PropertiesProvider.RegisterBioEngineContentProperties<SeoContentPropertiesSet>("seo");
            PropertiesProvider.RegisterBioEngineProperties<SeoSitePropertiesSet, Site>("seosite");
        }
    }
}
