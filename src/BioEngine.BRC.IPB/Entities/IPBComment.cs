using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Comments;
using BioEngine.BRC.Core.Entities;

namespace BioEngine.BRC.IPB.Entities
{
    [Entity("ipbcomments", "IPB Коммент")]
    public class IPBComment : BaseComment
    {
        [Required] public int PostId { get; set; }
        [Required] public int TopicId { get; set; }
        [NotMapped] public override string Url { get; set; } = string.Empty;
    }
}
