using System;

namespace BioEngine.BRC.Core.Entities.Abstractions
{
    public interface ISiteEntity : IBioEntity
    {
        Guid[] SiteIds { get; set; }
    }
}
