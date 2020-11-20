using System.Collections.Generic;
using BioEngine.BRC.Core.Publishing;

namespace BioEngine.BRC.Twitter
{
    public class TwitterPublishConfig : IContentPublisherConfig
    {
        public TwitterConfig Config { get; }
        public List<string> Tags { get; }

        public TwitterPublishConfig(TwitterConfig config, List<string> tags)
        {
            Config = config;
            Tags = tags;
        }
    }
}
