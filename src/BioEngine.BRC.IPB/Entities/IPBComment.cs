using System.ComponentModel.DataAnnotations;
using BioEngine.BRC.Core.Comments;
using BioEngine.BRC.Core.Entities;

namespace BioEngine.BRC.IPB.Entities
{
    [Entity("ipbcomments")]
    public class IPBComment : BaseComment
    {
        [Required] public int PostId { get; set; }
        [Required] public int TopicId { get; set; }
    }
}
