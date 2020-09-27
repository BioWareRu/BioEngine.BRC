using System;
using BioEngine.BRC.Core.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;
using Sitko.Core.App.Web;

namespace BioEngine.BRC.Web
{
    public class WebModule<T> : BaseApplicationModule<T>, IWebApplicationModule where T : WebModuleConfig, new()
    {
        public WebModule(T config, Application application) : base(config, application)
        {
        }

        public override void CheckConfig()
        {
            base.CheckConfig();
            if (Config.SiteId == null)
            {
                throw new ArgumentException("Site id can't be null");
            }
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            services.AddSingleton<WebModuleConfig>(Config);
            services.AddScoped<BaseControllerContext>();
            services.AddScoped(typeof(BaseControllerContext<,,>));
        }

        public void ConfigureEndpoints(IConfiguration configuration, IHostEnvironment environment,
            IApplicationBuilder appBuilder,
            IEndpointRouteBuilder endpoints)
        {
            endpoints.MapRoute("rss", "/rss.xml", "Rss", "Index");
        }
    }

    public abstract class WebModuleConfig
    {
        public Guid? SiteId { get; set; }
    }
}
