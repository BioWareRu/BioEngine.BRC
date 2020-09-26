using System;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using JetBrains.Annotations;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    [UsedImplicitly]
    public class PagesRepository : ContentEntityRepository<Page>
    {
        public PagesRepository(EFRepositoryContext<Page, Guid, BioContext> repositoryContext) : base(repositoryContext)
        {
        }
    }
}
