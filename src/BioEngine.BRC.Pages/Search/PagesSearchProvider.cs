using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Search;
using BioEngine.BRC.Pages.Entities;
using BioEngine.BRC.Pages.Repository;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Sitko.Core.Search;

namespace BioEngine.BRC.Pages.Search
{
    [UsedImplicitly]
    public class PagesSearchProvider : BRCSearchProvider<Page>
    {
        private readonly PagesRepository _pagesRepository;


        public PagesSearchProvider(PagesRepository pagesRepository,
            ILogger<BaseSearchProvider<Page, Guid, BRCSearchModel>> logger,
            ISearcher<BRCSearchModel>? searcher = null) : base(logger, searcher)
        {
            _pagesRepository = pagesRepository;
        }

        protected override Task<BRCSearchModel[]> GetSearchModelsAsync(Page[] entities,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(entities.Select(page =>
            {
                return new BRCSearchModel(page.Id.ToString(), page.Title, page.Url,
                    string.Join(" ", page.Blocks.Select(b => b.ToString()).Where(s => !string.IsNullOrEmpty(s))),
                    page.DateAdded) {SiteIds = page.SiteIds};
            }).ToArray());
        }

        protected override Task<Page[]> GetEntitiesAsync(BRCSearchModel[] searchModels,
            CancellationToken cancellationToken = default)
        {
            var ids = searchModels.Select(s => s.Id).Distinct().ToArray();
            return _pagesRepository.GetByIdsAsync(ids.Select(ParseId).ToArray(), cancellationToken);
            ;
        }
    }
}
