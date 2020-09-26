using BioEngine.BRC.IPB.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.IPB
{
    public class IPBSiteUsersModule : IPBUsersModule<IPBSiteUsersModuleConfig, IPBSiteCurrentUserProvider>
    {
        public IPBSiteUsersModule(IPBSiteUsersModuleConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);

            services.AddIpbOauthAuthentication(Config, environment);
        }
    }

    public class IPBSiteUsersModuleConfig : IPBUsersModuleConfig
    {
    }
}
