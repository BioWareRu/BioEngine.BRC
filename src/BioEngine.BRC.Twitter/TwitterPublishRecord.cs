using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Publishing;

namespace BioEngine.BRC.Twitter
{
    [Entity("twitterpublishrecord")]
    public class TwitterPublishRecord : BasePublishRecord
    {
        public long TweetId { get; set; }
    }
}
