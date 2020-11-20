using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Entities.Blocks;
using BioEngine.BRC.Core.Routing;
using BioEngine.BRC.Posts.Entities;
using BioEngine.BRC.Web;
using BioEngine.BRC.Web.Controllers;
using BioEngine.BRC.Web.Helpers;
using BioEngine.BRC.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Sitko.Core.Search;

namespace BioEngine.BRC.Site.Controllers
{
    [Route("/search")]
    public class SearchController : BaseSearchController
    {
        public SearchController(BaseControllerContext context, IEnumerable<ISearchProvider> searchProviders) : base(
            context, searchProviders)
        {
        }

        [HttpGet("")]
        public override async Task<IActionResult> IndexAsync([FromQuery] string query, string block)
        {
            var viewModel = new SearchViewModel(GetPageContext(), query);

            if (!string.IsNullOrEmpty(query))
            {
                var hasBlock = !string.IsNullOrEmpty(block);
                var limit = hasBlock ? 0 : 5;
                if (!hasBlock || block == "games")
                {
                    var searchBlock =
                        await BuildBlockAsync<Game, Guid>(query, limit, "Игры", "games");
                    if (searchBlock != null)
                    {
                        viewModel.AddBlock(searchBlock);
                    }
                }

                if (!hasBlock || block == "developers")
                {
                    var searchBlock = await BuildBlockAsync<Developer, Guid>(query, limit, "Разработчики", "developers");
                    if (searchBlock != null)
                    {
                        viewModel.AddBlock(searchBlock);
                    }
                }

                if (!hasBlock || block == "topics")
                {
                    var searchBlock =
                        await BuildBlockAsync<Topic, Guid>(query, limit, "Темы", "topics");
                    if (searchBlock != null)
                    {
                        viewModel.AddBlock(searchBlock);
                    }
                }

                if (!hasBlock || block == "posts")
                {
                    var searchBlock = await BuildBlockAsync<Post, Guid>(query, limit, "Публикации", "posts");
                    if (searchBlock != null)
                    {
                        viewModel.AddBlock(searchBlock);
                    }
                }
            }

            return View("Index", viewModel);
        }

        [SuppressMessage("ReSharper", "Mvc.ActionNotResolved")]
        private async Task<SearchBlock> BuildBlockAsync<TEntity, TEntityPk>(string query, int limit, string blockTitle,
            string blockKey)
            where TEntity : BaseEntity, IContentEntity
        {
            var entitiesCount = await CountEntitiesAsync<TEntity, TEntityPk>(query);
            if (entitiesCount > 0)
            {
                var entities = await SearchEntitiesAsync<TEntity, TEntityPk>(query, limit);
                var searchBlock = CreateSearchBlock(blockTitle,
                    new Uri(Url.Action("Index", "Search", new {query, block = blockKey}), UriKind.Relative),
                    entitiesCount,
                    entities, x => x.Title,
                    x => LinkGenerator.GeneratePublicUrl(x),
                    x => HtmlHelper.GetDescriptionFromHtml(
                        (x.Blocks.OrderBy(b => b.Position).FirstOrDefault(b => b is TextBlock) as TextBlock)
                        ?.Data
                        .Text), x => x.DateUpdated);
                return searchBlock;
            }

            return null;
        }
    }
}
