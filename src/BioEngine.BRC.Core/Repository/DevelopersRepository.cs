using System;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public class DevelopersRepository : SectionRepository<Developer>
    {
        public DevelopersRepository(EFRepositoryContext<Developer, Guid, BioContext> repositoryContext) : base(
            repositoryContext)
        {
        }
    }
}
