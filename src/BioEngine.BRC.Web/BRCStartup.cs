using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App.Web;

namespace BioEngine.BRC.Web
{
    public class BRCStartup<TApplication> : BaseStartup<TApplication> where TApplication: WebApplication<TApplication>
    {
        public BRCStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
            
        }
    }
}
