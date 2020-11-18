namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("twitterblock","Twitter")]
    public class TwitterBlock : ContentBlock<TwitterBlockData>
    {
        public override string ToString()
        {
            return $"Twitter: {Data.TweetId} by {Data.TweetAuthor}";
        }
    }

    public class TwitterBlockData : ContentBlockData
    {
        public string TweetId { get; set; } = "";
        public string TweetAuthor { get; set; } = "";
    }
}
