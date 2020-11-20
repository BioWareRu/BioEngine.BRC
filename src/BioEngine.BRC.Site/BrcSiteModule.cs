using System;
using BioEngine.BRC.Core.Users;
using BioEngine.BRC.Site.Patreon;
using BioEngine.BRC.Web;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.Site
{
    public class BrcSiteModule : WebModule<BrcSiteModuleConfig>
    {
        public BrcSiteModule(BrcSiteModuleConfig config, Application application) : base(config, application)
        {
        }


        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            services.AddSingleton(Config);
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            if (environment.IsDevelopment())
            {
                services.AddScoped<IUserDataProvider, TestUserDataProvider>();
            }

            services.AddSingleton<PatreonApiHelper>();
            services.Configure<PatreonConfig>(o =>
            {
                o.ServiceUrl = new Uri(Config.PatreonServiceUrl);
            });
        }
    }

    public class BrcSiteModuleConfig : WebModuleConfig
    {
        public Guid? SiteId { get; set; }
        public string PatreonServiceUrl { get; set; } = "http://localhost";
    }
}
