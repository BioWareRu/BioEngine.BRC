using System;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Comments;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Repository;
using BioEngine.BRC.Posts.Entities;
using BioEngine.BRC.Posts.Repository;
using BioEngine.BRC.Web;
using BioEngine.BRC.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Sitko.Core.Repository;

namespace BioEngine.BRC.Posts.Web
{
    public abstract class BasePostsController : SiteController<Post, Guid, PostsRepository>
    {
        protected readonly TagsRepository TagsRepository;
        private readonly ICommentsProvider _commentsProvider;

        protected BasePostsController(
            BaseControllerContext<Post, Guid, PostsRepository> context,
            TagsRepository tagsRepository,
            ICommentsProvider commentsProvider) : base(context)
        {
            TagsRepository = tagsRepository;
            _commentsProvider = commentsProvider;
        }

        public override async Task<IActionResult> ShowAsync(string url)
        {
            var post = await Repository.GetWithBlocksAsync(async entities =>
                (await ApplyPublishConditionsAsync(entities)).Where(e => e.Url == url));
            if (post == null)
            {
                return PageNotFound();
            }

            var commentsData = await _commentsProvider.GetCommentsDataAsync(new IContentItem[] {post}, Site);

            return View(new PostViewModel(GetPageContext(), post, commentsData[post.Id].count,
                commentsData[post.Id].uri, ContentEntityViewMode.Entity));
        }

        public virtual Task<IActionResult> ListByTagPageAsync(string tagNames, int page)
        {
            return ShowListByTagAsync(tagNames, page);
        }

        public virtual Task<IActionResult> ListByTagAsync(string tagNames)
        {
            return ShowListByTagAsync(tagNames, 0);
        }

        protected virtual async Task<IActionResult> ShowListByTagAsync(string tagNames, int page)
        {
            if (string.IsNullOrEmpty(tagNames))
            {
                return BadRequest();
            }

            var titles = tagNames.Split("+").Select(t => t.ToLowerInvariant()).ToArray();

            var tags = await TagsRepository.GetAllAsync(q => q.Where(t => titles.Contains(t.Title.ToLower())));
            if (!tags.items.Any())
            {
                return PageNotFound();
            }

            var (items, itemsCount) =
                await Repository.GetAllWithBlocksAsync(async entities =>
                    (await ConfigureQueryAsync(entities, page)).WithTags(tags.items).Where(e => e.IsPublished));
            return View("List", new ListViewModel<Post>(GetPageContext(), items,
                itemsCount, Page, ItemsPerPage) {Tags = tags.items});
        }

        protected override void ApplyDefaultOrder(IRepositoryQuery<Post> query)
        {
            query.OrderByDescending(p => p.IsPublished ? p.DatePublished : p.DateUpdated);
        }
    }
}
