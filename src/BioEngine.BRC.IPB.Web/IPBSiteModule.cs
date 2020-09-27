using BioEngine.BRC.Core.Comments;
using BioEngine.BRC.IPB.Web.Comments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.IPB.Web
{
    public class IPBSiteModule : IPBModule<IPBSiteModuleConfig>
    {
        public IPBSiteModule(IPBSiteModuleConfig config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            services.AddScoped<ICommentsProvider, IPBCommentsProvider>();
        }
    }

    public class IPBSiteModuleConfig : IPBModuleConfig
    {
    }
}
