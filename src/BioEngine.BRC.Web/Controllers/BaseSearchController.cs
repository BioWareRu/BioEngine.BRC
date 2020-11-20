using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Sitko.Core.Search;

namespace BioEngine.BRC.Web.Controllers
{
    public abstract class BaseSearchController : BaseSiteController
    {
        private readonly IEnumerable<ISearchProvider> _searchProviders;

        protected BaseSearchController(BaseControllerContext context, IEnumerable<ISearchProvider> searchProviders) :
            base(context)
        {
            _searchProviders = searchProviders;
        }

        public abstract Task<IActionResult> IndexAsync([FromQuery] string query, string block);

        private ISearchProvider<TEntity, TEntityPk>? GetSearchProvider<TEntity, TEntityPk>() where TEntity : BaseEntity
        {
            var provider = _searchProviders.FirstOrDefault(s => s.CanProcess(typeof(TEntity)));
            return provider as ISearchProvider<TEntity, TEntityPk>;
        }

        protected async Task<IEnumerable<TEntity>?> SearchEntitiesAsync<TEntity, TEntityPk>(string query, int limit = 0)
            where TEntity : BaseEntity
        {
            var provider = GetSearchProvider<TEntity, TEntityPk>();
            if (provider != null)
            {
                return await provider.SearchAsync(query, limit);
            }

            return null;
        }

        protected async Task<long> CountEntitiesAsync<TEntity, TEntityPk>(string query) where TEntity : BaseEntity
        {
            var provider = GetSearchProvider<TEntity, TEntityPk>();
            if (provider != null)
            {
                return await provider.CountAsync(query);
            }

            return 0;
        }

        protected SearchBlock CreateSearchBlock<T>(string title, Uri url, long totalCount,
            IEnumerable<T> items,
            Func<T, string> getTitle, Func<T, Uri> getUrl, Func<T, string> getDesc, Func<T, DateTimeOffset> getDate)
        {
            var block = new SearchBlock(title, url, totalCount);
            foreach (var item in items)
            {
                block.AddItem(getTitle(item), getUrl(item), getDesc(item), getDate(item));
            }

            return block;
        }
    }
}
