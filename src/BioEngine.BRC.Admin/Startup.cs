using System;
using BioEngine.BRC.Admin.Components;
using BioEngine.BRC.Admin.Components.RenderService;
using BioEngine.BRC.Admin.Components.Validation;
using BioEngine.BRC.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BioEngine.BRC.Core.Publishing;
using BioEngine.BRC.Core.Routing;
using Microsoft.AspNetCore.Routing;
using Radzen;
using Sitko.Core.App.Web;

namespace BioEngine.BRC.Admin
{
    public class Startup : BaseStartup<BRCApplication>
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        protected override void ConfigureAppServices(IServiceCollection services)
        {
            base.ConfigureAppServices(services);
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/");
            });
            services.AddServerSideBlazor().AddCircuitOptions(options =>
            {
                options.DetailedErrors = Environment.IsDevelopment();
            });
            services.AddScoped<IContentRender, ContentRender>();
            services.AddScoped<BRCPostsPublisher>();
            services.Configure<BrcAdminOptions>(options =>
            {
                options.DefaultMainSiteId = Guid.Parse(Configuration["BE_DEFAULT_MAIN_SITE_ID"]);
            });
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<ContextMenuService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<FluentValidator>();
        }

        protected override void ConfigureAfterRoutingMiddleware(IApplicationBuilder app)
        {
            base.ConfigureAfterRoutingMiddleware(app);
            app.UseAuthentication();
            app.UseAuthorization();
        }

        protected override void ConfigureEndpoints(IApplicationBuilder app, IEndpointRouteBuilder endpoints)
        {
            base.ConfigureEndpoints(app, endpoints);
            endpoints.AddBrcRoutes();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        }
    }

    public class BrcAdminOptions
    {
        public Guid DefaultMainSiteId { get; set; }
    }
}
