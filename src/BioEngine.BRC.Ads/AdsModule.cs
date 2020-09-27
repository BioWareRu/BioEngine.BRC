using BioEngine.BRC.Ads.Entities;
using BioEngine.BRC.Ads.Policies;
using BioEngine.BRC.Core;
using BioEngine.BRC.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.Ads
{
    public abstract class AdsModule : BaseApplicationModule<AdsModuleConfig>
    {
        protected AdsModule(AdsModuleConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            var registrar = BRCEntitiesRegistrar.Instance(services);
            registrar.RegisterSiteEntity<Ad>();
            services.AddAuthorization(options =>
                {
                    options.AddPolicy(AdsPolicies.Ads, BRCApplication.AdminPolicy);
                    options.AddPolicy(AdsPolicies.AdsAdd, BRCApplication.AdminPolicy);
                    options.AddPolicy(AdsPolicies.AdsEdit, BRCApplication.AdminPolicy);
                    options.AddPolicy(AdsPolicies.AdsDelete, BRCApplication.AdminPolicy);
                    options.AddPolicy(AdsPolicies.AdsPublish, BRCApplication.AdminPolicy);
            });
            
        }
    }

    public class AdsModuleConfig
    {
    }
}
