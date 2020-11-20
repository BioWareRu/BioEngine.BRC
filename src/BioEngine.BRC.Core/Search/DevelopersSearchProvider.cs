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
    public class DevelopersSearchProvider : SectionsSearchProvider<Developer>
    {
        private readonly DevelopersRepository _developersRepository;


        public DevelopersSearchProvider(DevelopersRepository developersRepository,
            ILogger<BaseSearchProvider<Developer, Guid, BRCSearchModel>> logger,
            ISearcher<BRCSearchModel>? searcher = null) : base(logger, searcher)
        {
            _developersRepository = developersRepository;
        }


        protected override Task<Developer[]> GetEntitiesAsync(BRCSearchModel[] searchModels,
            CancellationToken cancellationToken = default)
        {
            var ids = searchModels.Select(s => s.Id).Distinct().ToArray();
            return _developersRepository.GetByIdsAsync(ids.Select(ParseId).ToArray(), cancellationToken);
        }
    }
}
