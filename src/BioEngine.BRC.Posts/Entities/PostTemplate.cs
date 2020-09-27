using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Users;

namespace BioEngine.BRC.Posts.Entities
{
    [Table("PostTemplates")]
    [Entity("posttemplate")]
    public class PostTemplate : BaseEntity
    {
        [Required] public string Title { get; set; }
        [Required] public Guid[] SectionIds { get; set; } = new Guid[0];
        [Required] public Guid[] TagIds { get; set; } = new Guid[0];
        [Required] public virtual string AuthorId { get; set; }

        [Column(TypeName = "jsonb")]
        [Required]
        public PostTemplateData Data { get; set; }

        [NotMapped] public IUser Author { get; set; }
    }

    public class PostTemplateData
    {
        public List<ContentBlock> Blocks { get; set; } = new List<ContentBlock>();
        public string Url { get; set; }
        public string Title { get; set; }
    }
}
