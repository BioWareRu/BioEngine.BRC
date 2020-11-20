using System;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public abstract class SectionRepository<TEntity> : ContentEntityRepository<TEntity> where TEntity : Section
    {
        protected SectionRepository(EFRepositoryContext<TEntity, Guid, BioContext> repositoryContext) : base(
            repositoryContext)
        {
        }
    }
}
