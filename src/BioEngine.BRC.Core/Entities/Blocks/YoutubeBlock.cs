namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("youtubeblock", "Youtube")]
    public class YoutubeBlock : ContentBlock<YoutubeBlockData>
    {
        public override string ToString()
        {
            return $"Youtube: {Data.YoutubeId}";
        }
    }

    public class YoutubeBlockData : ContentBlockData
    {
        public string YoutubeId { get; set; } = "";
    }
}
