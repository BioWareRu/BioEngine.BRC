using System;
using BioEngine.BRC.Core;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Routing;
using BioEngine.BRC.Pages.Entities;
using BioEngine.BRC.Pages.Policies;
using BioEngine.BRC.Pages.Search;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;
using Sitko.Core.App.Web;
using Sitko.Core.Search;

namespace BioEngine.BRC.Pages
{
    public class PagesModule<T> : BaseApplicationModule<T>, IWebApplicationModule where T : PagesModuleConfig, new()
    {
        public PagesModule(T config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);

            services.RegisterSearchProvider<PagesSearchProvider, Page, Guid>();
            var registrar = BRCEntitiesRegistrar.Instance(services);
            registrar.RegisterSiteEntity<Page>(builder =>
            {
                builder.HasIndex(p => p.IsPublished);
                builder.HasIndex(p => p.Url).IsUnique();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PagesPolicies.Pages, BRCApplication.AdminPolicy);
                options.AddPolicy(PagesPolicies.PagesAdd, BRCApplication.AdminPolicy);
                options.AddPolicy(PagesPolicies.PagesEdit, BRCApplication.AdminPolicy);
                options.AddPolicy(PagesPolicies.PagesDelete, BRCApplication.AdminPolicy);
                options.AddPolicy(PagesPolicies.PagesPublish, BRCApplication.AdminPolicy);
            });
            // pages
        }

        public void ConfigureEndpoints(IConfiguration configuration, IHostEnvironment environment,
            IApplicationBuilder appBuilder,
            IEndpointRouteBuilder endpoints)
        {
            endpoints.MapRoute(BioEnginePagesRoutes.Page, "/pages/{url}.html", "Pages", "Show");
        }
    }

    public abstract class PagesModuleConfig
    {
    }
}
