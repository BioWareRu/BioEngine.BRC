using BioEngine.BRC.Core;
using BioEngine.BRC.Core.Properties;
using Microsoft.AspNetCore.Routing;
using Sitko.Core.Storage;

namespace BioEngine.BRC.Web.Model
{
    public class PageViewModelContext
    {
        public PageViewModelContext(LinkGenerator linkGenerator, PropertiesProvider propertiesProvider,
            Core.Entities.Site site, IStorage<BRCStorageConfig> storage, Core.Entities.Section? section = null)
        {
            LinkGenerator = linkGenerator;
            PropertiesProvider = propertiesProvider;
            Site = site;
            Storage = storage;
            Section = section;
        }

        public LinkGenerator LinkGenerator { get; }
        public PropertiesProvider PropertiesProvider { get; }
        public Core.Entities.Site Site { get; }
        public IStorage<BRCStorageConfig> Storage { get; }
        public Core.Entities.Section? Section { get; }
    }
}
