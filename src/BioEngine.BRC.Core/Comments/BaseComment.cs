using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Users;
using JetBrains.Annotations;

namespace BioEngine.BRC.Core.Comments
{
    [UsedImplicitly]
    public abstract class BaseComment : BaseEntity, IRoutable
    {
        [Required] public string ContentType { get; set; } = string.Empty;
        [Required] public Guid ContentId { get; set; }
        [Required] public string AuthorId { get; set; } = string.Empty;
        [Required] public Guid[] SiteIds { get; set; } = new Guid[0];
        public Guid? ReplyTo { get; set; }
        public string? Text { get; set; }

        [NotMapped] public IUser? Author { get; set; }

        [NotMapped] public IContentItem? ContentItem { get; set; }
        [NotMapped] public string PublicRouteName { get; set; } = BioEngineCommentsRoutes.Comment;
        [NotMapped] public abstract string Url { get; set; }
    }
}
