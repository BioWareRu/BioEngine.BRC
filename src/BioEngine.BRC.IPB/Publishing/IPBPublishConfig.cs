using BioEngine.BRC.Core.Publishing;

namespace BioEngine.BRC.IPB.Publishing
{
    public class IPBPublishConfig : IContentPublisherConfig
    {
        public IPBPublishConfig(int forumId, string authorId)
        {
            ForumId = forumId;
            AuthorId = authorId;
        }

        public int ForumId { get; }
        public string AuthorId { get; }
    }
}
