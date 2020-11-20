using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Repository;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Sitko.Core.Search;

namespace BioEngine.BRC.Core.Search
{
    [UsedImplicitly]
    public class TopicsSearchProvider : SectionsSearchProvider<Topic>
    {
        private readonly TopicsRepository _topicsRepository;

        public TopicsSearchProvider(TopicsRepository topicsRepository,
            ILogger<BaseSearchProvider<Topic, Guid, BRCSearchModel>> logger,
            ISearcher<BRCSearchModel>? searcher = null) : base(
            logger, searcher)
        {
            _topicsRepository = topicsRepository;
        }

        protected override Task<Topic[]> GetEntitiesAsync(BRCSearchModel[] searchModels,
            CancellationToken cancellationToken = default)
        {
            var ids = searchModels.Select(s => s.Id).Distinct().ToArray();
            return _topicsRepository.GetByIdsAsync(ids.Select(ParseId).ToArray(), cancellationToken);
        }
    }
}
