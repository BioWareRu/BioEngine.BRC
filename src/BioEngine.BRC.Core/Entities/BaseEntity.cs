using System;
using BioEngine.BRC.Core.Entities.Abstractions;

namespace BioEngine.BRC.Core.Entities
{
    public abstract class BaseEntity : Sitko.Core.Repository.Entity<Guid>, IBioEntity
    {
        public virtual DateTimeOffset DateAdded { get; set; } = DateTimeOffset.UtcNow;
        public virtual DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.UtcNow;
    }

    public abstract class BaseSiteEntity : BaseEntity, ISiteEntity
    {
        public virtual Guid[] SiteIds { get; set; } = new Guid[0];
    }
}
