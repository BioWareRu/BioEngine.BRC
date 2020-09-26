using BioEngine.BRC.IPB.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.IPB
{
    public class IPBApiUsersModule : IPBUsersModule<IPBApiUsersModuleConfig, IPBApiCurrentUserProvider>
    {
        public IPBApiUsersModule(IPBApiUsersModuleConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);

            services
                .AddAuthentication("ipbToken")
                .AddScheme<IPBTokenAuthOptions, TokenAuthenticationHandler>("ipbToken", null);
        }
    }

    public class IPBApiUsersModuleConfig : IPBUsersModuleConfig
    {
    }
}
