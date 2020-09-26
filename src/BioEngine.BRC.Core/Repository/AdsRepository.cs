using System;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public class AdsRepository : ContentEntityRepository<Ad>
    {
        public AdsRepository(EFRepositoryContext<Ad, Guid, BioContext> repositoryContext) : base(repositoryContext)
        {
        }
    }
}
