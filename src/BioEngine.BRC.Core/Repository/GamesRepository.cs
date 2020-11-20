using System;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public sealed class GamesRepository : SectionRepository<Game>
    {
        public GamesRepository(EFRepositoryContext<Game, Guid, BioContext> repositoryContext) : base(
            repositoryContext)
        {
        }
    }
}
