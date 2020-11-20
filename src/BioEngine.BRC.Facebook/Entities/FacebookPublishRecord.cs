using System.ComponentModel.DataAnnotations;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Publishing;

namespace BioEngine.BRC.Facebook.Entities
{
    [Entity("facebookpublishrecord", "Публикация в Facebook")]
    public class FacebookPublishRecord : BasePublishRecord
    {
        [Required] public string PostId { get; set; } = string.Empty;
    }
}
