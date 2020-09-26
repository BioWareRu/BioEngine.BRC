using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App.Web;

namespace BioEngine.BRC.Core.Web
{
    public class BRCStartup<TApplication> : Sitko.Core.App.Web.BaseStartup<TApplication> where TApplication: WebApplication<TApplication>
    {
        public BRCStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
            SetDefaultCulture("ru-RU");
        }
    }
}
