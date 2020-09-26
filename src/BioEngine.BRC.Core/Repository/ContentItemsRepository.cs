using System;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities.Abstractions;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public class ContentItemsRepository : SectionEntityRepository<IContentItem>
    {
        public ContentItemsRepository(EFRepositoryContext<IContentItem, Guid, BioContext> repositoryContext,
            SectionsRepository sectionsRepository) : base(repositoryContext, sectionsRepository)
        {
        }
    }
}
