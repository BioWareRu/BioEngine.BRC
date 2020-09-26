using BioEngine.BRC.Core.Properties;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Sitko.Core.Repository;
using Sitko.Core.Storage;

namespace BioEngine.BRC.Core.Web
{
    public class BaseControllerContext
    {
        public readonly LinkGenerator LinkGenerator;
        public readonly IStorage<BRCStorageConfig> Storage;
        public readonly PropertiesProvider PropertiesProvider;
        public ILogger Logger { get; }

        public BaseControllerContext(ILoggerFactory loggerFactory, IStorage<BRCStorageConfig> storage,
            PropertiesProvider propertiesProvider, LinkGenerator linkGenerator)
        {
            LinkGenerator = linkGenerator;
            Storage = storage;
            PropertiesProvider = propertiesProvider;
            Logger = loggerFactory.CreateLogger(GetType());
        }
    }

    public class BaseControllerContext<TEntity, TEntityPk, TRepository> : BaseControllerContext
        where TEntity : class, IEntity<TEntityPk>
        where TRepository : IRepository<TEntity, TEntityPk>
    {
        public TRepository Repository { get; }

        public BaseControllerContext(ILoggerFactory loggerFactory, IStorage<BRCStorageConfig> storage,
            PropertiesProvider propertiesProvider, LinkGenerator linkGenerator,
            TRepository repository) : base(loggerFactory, storage, propertiesProvider,
            linkGenerator)
        {
            Repository = repository;
        }
    }
}
