namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("twitchblock")]
    public class TwitchBlock : ContentBlock<TwitchBlockData>
    {
        public override string? TypeTitle { get; set; } = "Twitch";

        public override string ToString()
        {
            return $"Twitch: {Data.VideoId}{Data.ChannelId}{Data.CollectionId}";
        }
    }

    public class TwitchBlockData : ContentBlockData
    {
        public string VideoId { get; set; }
        public string ChannelId { get; set; }
        public string CollectionId { get; set; }
    }
}
