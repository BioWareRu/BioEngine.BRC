using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioEngine.BRC.Core.Entities.Abstractions
{
    public interface IContentItem : IContentEntity, ISectionEntity
    {
        [NotMapped] List<Section> Sections { get; set; }
        [NotMapped] List<Site> Sites { get; set; }
    }
}
