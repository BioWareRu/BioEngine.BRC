using System;
using BioEngine.BRC.Core.Entities;
using Microsoft.Extensions.Logging;
using Sitko.Core.Search;

namespace BioEngine.BRC.Core.Search
{
    public abstract class BRCSearchProvider<TEntity> : BaseSearchProvider<TEntity, Guid, BRCSearchModel>
        where TEntity : BaseEntity
    {
        protected BRCSearchProvider(ILogger<BaseSearchProvider<TEntity, Guid, BRCSearchModel>> logger,
            ISearcher<BRCSearchModel>? searcher = null) : base(logger, searcher)
        {
        }

        protected override Guid ParseId(string id)
        {
            return Guid.Parse(id);
        }

        protected override string GetId(TEntity entity)
        {
            return entity.Id.ToString();
        }
    }
}
