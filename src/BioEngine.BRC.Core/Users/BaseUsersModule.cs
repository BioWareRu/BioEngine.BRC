using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.Core.Users
{
    public abstract class
        BaseUsersModule<TConfig, TUserDataProvider, TCurrentUserProvider> : BaseApplicationModule<TConfig>
        where TConfig : BaseUsersModuleConfig, new()
        where TUserDataProvider : class, IUserDataProvider
        where TCurrentUserProvider : class, ICurrentUserProvider
    {
        protected BaseUsersModule(TConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);

            services.AddScoped<IUserDataProvider, TUserDataProvider>();
            services.AddScoped<ICurrentUserProvider, TCurrentUserProvider>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(BioPolicies.User, builder => builder.RequireAuthenticatedUser());
                foreach (var policy in Config.Policies)
                {
                    options.AddPolicy(policy.Key, policy.Value);
                }
            });
        }
    }

    public abstract class BaseUsersModuleConfig
    {
        public Dictionary<string, AuthorizationPolicy> Policies { get; } =
            new Dictionary<string, AuthorizationPolicy>();
    }
}
