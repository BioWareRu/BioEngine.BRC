using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Repository;
using BioEngine.BRC.Core.Search;
using BioEngine.BRC.Posts.Entities;
using BioEngine.BRC.Posts.Repository;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Sitko.Core.Search;

namespace BioEngine.BRC.Posts.Search
{
    [UsedImplicitly]
    public class PostsSearchProvider : BRCSearchProvider<Post>
    {
        private readonly TagsRepository _tagsRepository;
        private readonly PostsRepository _postsRepository;


        public PostsSearchProvider(TagsRepository tagsRepository, PostsRepository postsRepository,
            ILogger<BaseSearchProvider<Post, Guid, BRCSearchModel>> logger,
            ISearcher<BRCSearchModel>? searcher = null) : base(logger, searcher)
        {
            _tagsRepository = tagsRepository;
            _postsRepository = postsRepository;
        }

        protected override async Task<BRCSearchModel[]> GetSearchModelsAsync(Post[] entities,
            CancellationToken cancellationToken = default)
        {
            var tagIds = entities.SelectMany(e => e.TagIds).Distinct().ToArray();
            var tags = await _tagsRepository.GetByIdsAsync(tagIds);
            return entities.Select(post =>
            {
                var model =
                    new BRCSearchModel(post.Id.ToString(), post.Title, post.Url,
                        string.Join(" ", post.Blocks.Select(b => b.ToString()).Where(s => !string.IsNullOrEmpty(s))),
                        post.DateAdded)
                    {
                        SectionIds = post.SectionIds,
                        AuthorId = (post.AuthorId != null ? post.AuthorId.ToString() : string.Empty)!,
                        SiteIds = post.SiteIds,
                        Tags = tags.Where(t => post.TagIds.Contains(t.Id)).Select(t => t.Title).ToArray()
                    };

                return model;
            }).ToArray();
        }

        protected override Task<Post[]> GetEntitiesAsync(BRCSearchModel[] searchModels,
            CancellationToken cancellationToken = default)
        {
            var ids = searchModels.Select(s => s.Id).Distinct().ToArray();
            return _postsRepository.GetByIdsAsync(ids.Select(ParseId).ToArray(), cancellationToken);
        }
    }
}
