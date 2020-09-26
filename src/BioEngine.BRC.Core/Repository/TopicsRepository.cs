using System;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public class TopicsRepository : SectionRepository<Topic>
    {
        public TopicsRepository(EFRepositoryContext<Topic, Guid, BioContext> repositoryContext) : base(
            repositoryContext)
        {
        }
    }
}
