using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Sitko.Core.Search;

namespace BioEngine.BRC.Core.Search
{
    [UsedImplicitly]
    public abstract class SectionsSearchProvider<TSection> : BRCSearchProvider<TSection> where TSection : Section
    {
        protected SectionsSearchProvider(
            ILogger<BaseSearchProvider<TSection, Guid, BRCSearchModel>> logger,
            ISearcher<BRCSearchModel>? searcher = null) : base(logger, searcher)
        {
        }

        protected override Task<BRCSearchModel[]> GetSearchModelsAsync(TSection[] entities,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(entities.Select(section =>
            {
                return new BRCSearchModel(section.Id.ToString(), section.Title, section.Url,
                    string.Join(" ", section.Blocks.Select(b => b.ToString()).Where(s => !string.IsNullOrEmpty(s))),
                    section.DateAdded) {SiteIds = section.SiteIds};
            }).ToArray());
        }
    }
}
