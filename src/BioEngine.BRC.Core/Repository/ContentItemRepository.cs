using System;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities.Abstractions;
using Sitko.Core.Repository;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public abstract class ContentItemRepository<TEntity> : SectionEntityRepository<TEntity>
        where TEntity : class, IContentItem, IEntity, ISiteEntity, ISectionEntity
    {
        protected ContentItemRepository(EFRepositoryContext<TEntity, Guid, BioContext> repositoryContext,
            SectionsRepository sectionsRepository) : base(repositoryContext, sectionsRepository)
        {
        }
    }
}
