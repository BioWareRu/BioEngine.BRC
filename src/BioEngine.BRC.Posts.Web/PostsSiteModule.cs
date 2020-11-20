using BioEngine.BRC.Posts.Web.Rss;
using BioEngine.BRC.Posts.Web.SiteMaps;
using BioEngine.BRC.Web.Rss;
using cloudscribe.Web.SiteMap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.Posts.Web
{
    public class PostsSiteModule : PostsModule<PostsSiteModuleConfig>
    {
        public PostsSiteModule(PostsSiteModuleConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            services.AddScoped<ISiteMapNodeService, PostsSiteMapNodeService>();
            services.AddScoped<IRssItemsProvider, PostsRssItemsProvider>();
        }
    }

    public class PostsSiteModuleConfig : PostsModuleConfig
    {
        
    }
}
