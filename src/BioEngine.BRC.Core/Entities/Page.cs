using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Routing;

namespace BioEngine.BRC.Core.Entities
{
    [Entity("pagecontentitem")]
    [Table("Pages")]
    public class Page : BaseSiteEntity, IContentEntity
    {
        [NotMapped] public string PublicRouteName { get; set; } = BioEnginePagesRoutes.Page;
        public string Url { get; set; }
        public string Title { get; set; }
        [NotMapped] public List<ContentBlock> Blocks { get; set; } = new List<ContentBlock>();
        public bool IsPublished { get; set; }
        public DateTimeOffset? DatePublished { get; set; }
    }
}
