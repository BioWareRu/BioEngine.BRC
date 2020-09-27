using System;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Entities.Blocks;
using BioEngine.BRC.Core.Routing;
using BioEngine.BRC.Seo;
using BioEngine.BRC.Web.Helpers;

namespace BioEngine.BRC.Web.Model
{
    public class EntityViewModel<TEntity> : PageViewModel where TEntity : class, IContentEntity
    {
        public TEntity Entity { get; }
        public ContentEntityViewMode Mode { get; }

        public EntityViewModel(PageViewModelContext context, TEntity entity,
            ContentEntityViewMode mode = ContentEntityViewMode.List)
            : base(context)
        {
            Entity = entity;
            Mode = mode;
        }

        public override async Task<PageMetaModel> GetMetaAsync()
        {
            var path = LinkGenerator.GeneratePublicUrl(Entity);
            var meta = new PageMetaModel
            {
                Title = $"{Entity.Title} / {Site.Title}", CurrentUrl = new Uri($"{Site.Url}{path}")
            };

            var seoPropertiesSet = await PropertiesProvider.GetAsync<SeoContentPropertiesSet>(Entity);
            if (seoPropertiesSet != null && !string.IsNullOrEmpty(seoPropertiesSet.Description))
            {
                meta.Description = seoPropertiesSet.Description;
            }
            else
            {
                if (Entity is IContentEntity contentEntity)
                {
                    if (contentEntity.Blocks.OrderBy(b => b.Position).FirstOrDefault(b => b is TextBlock) is TextBlock
                        textBlock)
                    {
                        meta.Description = HtmlHelper.GetDescriptionFromHtml(textBlock.Data.Text);
                    }
                }
            }

            if (seoPropertiesSet != null && !string.IsNullOrEmpty(seoPropertiesSet.Keywords))
            {
                meta.Keywords = seoPropertiesSet.Keywords;
            }
            else
            {
                if (Entity is ITaggedContentEntity taggedContentEntity)
                {
                    meta.Keywords = string.Join(", ", taggedContentEntity.Tags.Select(t => t.Title));
                }
            }

            if (Entity is IContentEntity contEntity)
            {
                foreach (var block in contEntity.Blocks.OrderBy(b => b.Position))
                {
                    if (block is PictureBlock pictureBlock)
                    {
                        meta.ImageUrl = Storage.PublicUri(pictureBlock.Data.Picture);
                        break;
                    }

                    if (block is GalleryBlock galleryBlock && galleryBlock.Data.Pictures.Length > 0)
                    {
                        meta.ImageUrl = Storage.PublicUri(galleryBlock.Data.Pictures[0]);
                        break;
                    }
                }
            }

            return meta;
        }

        public EntityViewModel<IContentEntity> ContentEntityViewModel()
        {
            if (Entity is IContentEntity contentEntity)
            {
                return new EntityViewModel<IContentEntity>(
                    new PageViewModelContext(LinkGenerator, PropertiesProvider, Site, Storage, Section),
                    contentEntity,
                    Mode);
            }

            throw new ArgumentException($"Entity {Entity} is not IContentEntity");
        }
    }
}
