using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Core;
using BioEngine.BRC.Core.Comments;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Entities.Blocks;
using BioEngine.BRC.Core.Routing;
using BioEngine.BRC.Posts.Entities;
using BioEngine.BRC.Posts.Repository;
using BioEngine.BRC.Web.Rss;
using cloudscribe.Syndication.Models.Rss;
using Microsoft.AspNetCore.Routing;
using Sitko.Core.Storage;

namespace BioEngine.BRC.Posts.Web.Rss
{
    public class PostsRssItemsProvider : IRssItemsProvider
    {
        private readonly PostsRepository _postsRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly ICommentsProvider _commentsProvider;
        private readonly IStorage<BRCStorageConfig> _storage;

        public PostsRssItemsProvider(PostsRepository postsRepository, LinkGenerator linkGenerator,
            ICommentsProvider commentsProvider, IStorage<BRCStorageConfig> storage)
        {
            _postsRepository = postsRepository;
            _linkGenerator = linkGenerator;
            _commentsProvider = commentsProvider;
            _storage = storage;
        }

        public async Task<IEnumerable<RssItem>> GetItemsAsync(Site site, int count)
        {
            var posts = await _postsRepository.GetAllWithBlocksAsync(entities =>
                entities.Where(e => e.IsPublished).ForSite(site).OrderByDescending(p => p.DatePublished).Take(count));
            DateTimeOffset? mostRecentPubDate = DateTimeOffset.MinValue;
            var commentsData =
                await _commentsProvider.GetCommentsDataAsync(posts.items.Select(p => p as IContentItem).ToArray(),
                    site);
            var items = new List<RssItem>();
            foreach (var post in posts.items)
            {
                if (post.DatePublished != null)
                {
                    var newsDate = post.DatePublished;
                    if (newsDate > mostRecentPubDate) mostRecentPubDate = newsDate;
                    var postUrl = _linkGenerator.GeneratePublicUrl(post, site);
                    var item = new RssItem
                    {
                        Title = post.Title,
                        Description = GetDescription(post),
                        Link = postUrl,
                        PublicationDate = newsDate.Value.DateTime,
                        Author = post.Author.Name,
                        Guid = new RssGuid(postUrl.ToString(), true)
                    };

                    if (commentsData.ContainsKey(post.Id))
                    {
                        item.Comments = commentsData[post.Id].uri;
                    }

                    foreach (var section in post.Sections)
                    {
                        item.Categories.Add(new RssCategory(section.Title,
                            _linkGenerator.GeneratePublicUrl(section).ToString()));
                    }

                    items.Add(item);
                }
            }

            return items;
        }

        private string GetDescription(Post post)
        {
            var description = "";

            foreach (var block in post.Blocks.OrderBy(b => b.Position))
            {
                switch (block)
                {
                    case CutBlock _:
                        return description;
                    case TextBlock textBlock:
                        description += textBlock.Data.Text;
                        break;
                    case QuoteBlock quoteBlock:
                        description +=
                            $"<blockquote><div>{quoteBlock.Data.Text}</div><cite>{quoteBlock.Data.Author}</cite></blockquote>";
                        break;
                    case PictureBlock pictureBlock:
                        description +=
                            $"<p style=\"text-align:center;\"><img src=\"{_storage.PublicUri(pictureBlock.Data.Picture)}\" alt=\"{pictureBlock.Data.Picture.FileName}\" /></p>";
                        break;
                    case GalleryBlock galleryBlock:
                        foreach (var picture in galleryBlock.Data.Pictures)
                        {
                            description +=
                                $"<p style=\"text-align:center;\"><img src=\"{_storage.PublicUri(picture)}\" alt=\"{picture.FileName}\" /></p>";
                        }

                        break;
                    default:
                        description +=
                            $"<p style=\"text-align:center;\"><a href=\"{_linkGenerator.GeneratePublicUrl(post)}\">Посмотреть на сайте</a></p>";
                        break;
                }
            }

            return description;
        }
    }
}
