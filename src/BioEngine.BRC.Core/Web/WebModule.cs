using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace BioEngine.BRC.Core.Web
{
    public class WebModule<T> : BaseApplicationModule<T> where T : WebModuleConfig, new()
    {
        public WebModule(T config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            services.AddScoped<BaseControllerContext>();
            services.AddScoped(typeof(BaseControllerContext<,,>));
        }
    }

    public abstract class WebModuleConfig
    {
    }
}
