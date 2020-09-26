using System.ComponentModel.DataAnnotations;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Publishing;

namespace BioEngine.BRC.IPB.Publishing
{
    [Entity("ipbpublishrecord")]
    public class IPBPublishRecord : BasePublishRecord
    {
        [Required] public int TopicId { get; set; }
        [Required] public int PostId { get; set; }
    }
}
