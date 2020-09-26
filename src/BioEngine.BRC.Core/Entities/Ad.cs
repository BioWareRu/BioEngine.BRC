using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Entities.Abstractions;

namespace BioEngine.BRC.Core.Entities
{
    [Table("Ads")]
    [Entity("ad")]
    public class Ad : BaseSiteEntity, IContentEntity
    {
        [NotMapped] public string PublicRouteName { get; set; } = "";
        [Required] public string Url { get; set; }
        public string Title { get; set; }
        [NotMapped] public List<ContentBlock> Blocks { get; set; } = new List<ContentBlock>();
        public bool IsPublished { get; set; }
        public DateTimeOffset? DatePublished { get; set; }
    }
}
