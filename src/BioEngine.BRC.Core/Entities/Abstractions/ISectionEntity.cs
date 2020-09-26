using System;
using System.Collections.Generic;
using Sitko.Core.Repository;

namespace BioEngine.BRC.Core.Entities.Abstractions
{
    public interface ISectionEntity : IEntity
    {
        Guid[] SectionIds { get; set; }
        Guid[] TagIds { get; set; }
        List<Tag> Tags { get; set; }
    }
}
