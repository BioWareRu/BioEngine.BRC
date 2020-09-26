using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public class PostTemplatesRepository<TUserPk> : EFRepository<PostTemplate, Guid, BioContext>
    {
        public PostTemplatesRepository(EFRepositoryContext<PostTemplate, Guid, BioContext> repositoryContext) :
            base(repositoryContext)
        {
        }

        public Task<(PostTemplate[] items, int itemsCount)> GetTemplatesAsync()
        {
            return GetAllAsync();
        }

        public async Task<PostTemplate> CreateTemplateAsync(Post content)
        {
            var template = new PostTemplate
            {
                Title = content.Title,
                AuthorId = content.AuthorId,
                Data = new PostTemplateData {Blocks = content.Blocks, Title = content.Title, Url = content.Url,},
                TagIds = content.TagIds,
                SectionIds = content.SectionIds,
            };

            var result = await AddAsync(template);
            if (!result.IsSuccess)
            {
                throw new Exception(result.ErrorsString);
            }

            return template;
        }

        public async Task<Post> CreateFromTemplateAsync(Guid templateId)
        {
            var template = await GetByIdAsync(templateId);
            if (template == null)
            {
                throw new ArgumentException(nameof(template));
            }


            var content = Activator.CreateInstance<Post>();
            content.Blocks = new List<ContentBlock>();
            foreach (var contentBlock in template.Data.Blocks)
            {
                contentBlock.Id = Guid.NewGuid();
                contentBlock.ContentId = Guid.Empty;
                content.Blocks.Add(contentBlock);
            }

            content.Url = template.Data.Url;
            content.Title = template.Data.Title;
            content.TagIds = template.TagIds;
            content.SectionIds = template.SectionIds;
            content.DateAdded = DateTimeOffset.UtcNow;
            content.DateUpdated = DateTimeOffset.UtcNow;

            return content;
        }
    }
}
