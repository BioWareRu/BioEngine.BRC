using System;
using BioEngine.BRC.Ads.Entities;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Repository;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Ads.Repository
{
    public class AdsRepository : ContentEntityRepository<Ad>
    {
        public AdsRepository(EFRepositoryContext<Ad, Guid, BioContext> repositoryContext) : base(repositoryContext)
        {
        }
    }
}
