using System.Globalization;
using BioEngine.BRC.Core.Routing;
using BioEngine.BRC.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App.Web;

namespace BioEngine.BRC.Site
{
    public abstract class BrcSiteStartup<TApplication> : BRCStartup<TApplication>
        where TApplication : WebApplication<TApplication>
    {
        protected BrcSiteStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration,
            environment)
        {
        }

        protected override IMvcBuilder ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            base.ConfigureMvc(mvcBuilder)
                .AddApplicationPart(typeof(BrcSiteModule).Assembly)
                .AddMvcOptions(options =>
                {
                    options.CacheProfiles.Add("SiteMapCacheProfile",
                        new CacheProfile {Duration = 600});
                });

            if (Environment.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }

            return mvcBuilder;
        }

        protected override void ConfigureAfterRoutingMiddleware(IApplicationBuilder app)
        {
            base.ConfigureAfterRoutingMiddleware(app);
            app.UseAuthentication();
            app.UseAuthorization();
        }

        protected override void ConfigureBeforeRoutingMiddleware(IApplicationBuilder app)
        {
            base.ConfigureBeforeRoutingMiddleware(app);
            
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseMiddleware<CurrentSiteMiddleware>();
            
            var supportedCultures = new[] {new CultureInfo("ru-RU"), new CultureInfo("ru")};

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru-RU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
        }

        protected override void ConfigureEndpoints(IApplicationBuilder app, IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.AddBrcRoutes();
            base.ConfigureEndpoints(app, endpointRouteBuilder);
        }
    }
}
