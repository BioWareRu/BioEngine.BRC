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
    public class GamesSearchProvider : SectionsSearchProvider<Game>
    {
        private readonly GamesRepository _gamesRepository;

        public GamesSearchProvider(
            GamesRepository gamesRepository, ILogger<BaseSearchProvider<Game, Guid, BRCSearchModel>> logger,
            ISearcher<BRCSearchModel>? searcher = null) : base(
            logger, searcher)
        {
            _gamesRepository = gamesRepository;
        }

        protected override Task<Game[]> GetEntitiesAsync(BRCSearchModel[] searchModels,
            CancellationToken cancellationToken = default)
        {
            var ids = searchModels.Select(s => s.Id).Distinct().ToArray();
            return _gamesRepository.GetByIdsAsync(ids.Select(ParseId).ToArray(), cancellationToken);
        }
    }
}
