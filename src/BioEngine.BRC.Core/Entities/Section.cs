using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Entities.Abstractions;

namespace BioEngine.BRC.Core.Entities
{
    [Table("Sections")]
    public abstract class Section : BaseSiteEntity, IContentEntity
    {
        public string Title { get; set; } = string.Empty;
        public virtual string Type { get; set; } = string.Empty;
        public virtual Guid? ParentId { get; set; }
        [NotMapped] public List<ContentBlock> Blocks { get; set; } = new List<ContentBlock>();

        public bool IsPublished { get; set; }
        public DateTimeOffset? DatePublished { get; set; }
        public string Url { get; set; } = string.Empty;

        [NotMapped] public abstract string PublicRouteName { get; set; }
    }

    public abstract class Section<T> : Section, ITypedEntity<T> where T : ITypedData, new()
    {
        [Column(TypeName = "jsonb")] public virtual T Data { get; set; } = new T();
    }
}
