using BioEngine.BRC.Core.Users;
using BioEngine.BRC.IPB.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.IPB
{
    public abstract class
        IPBUsersModule<TConfig, TCurrentUserProvider> : BaseUsersModule<TConfig, IPBUserDataProvider,
            TCurrentUserProvider> where TConfig : IPBUsersModuleConfig, new()
        where TCurrentUserProvider : class, ICurrentUserProvider
    {
        protected IPBUsersModule(TConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);

            services.AddSingleton(typeof(IPBUsersModuleConfig), Config);
            services.AddSingleton(Config);
            services.AddHttpClient();
        }
    }

    public abstract class IPBUsersModuleConfig : BaseUsersModuleConfig
    {
        public string ApiClientId { get; set; } = "";
        public string ApiClientSecret { get; set; } = "";
        public string CallbackPath { get; set; } = "";
        public string AuthorizationEndpoint { get; set; } = "";
        public string TokenEndpoint { get; set; } = "";
        public string DataProtectionPath { get; set; } = "";
        public bool DevMode { get; set; }
        public int AdminGroupId { get; set; }
        public int[] AdditionalGroupIds { get; set; }
    }
}
