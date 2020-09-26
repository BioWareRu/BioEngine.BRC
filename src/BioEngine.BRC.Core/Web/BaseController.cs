using BioEngine.BRC.Core.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Sitko.Core.Storage;

namespace BioEngine.BRC.Core.Web
{
    public abstract class BaseController : Controller
    {
        protected ILogger Logger { get; }
        protected IStorage<BRCStorageConfig> Storage { get; }
        protected PropertiesProvider PropertiesProvider { get; }
        protected LinkGenerator LinkGenerator { get; }

        protected BaseController(BaseControllerContext context)
        {
            Logger = context.Logger;
            Storage = context.Storage;
            PropertiesProvider = context.PropertiesProvider;
            LinkGenerator = context.LinkGenerator;
        }
    }
}
