using System;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public class ContentBlocksRepository : EFRepository<ContentBlock, Guid, BioContext>
    {
        public ContentBlocksRepository(EFRepositoryContext<ContentBlock, Guid, BioContext> repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
