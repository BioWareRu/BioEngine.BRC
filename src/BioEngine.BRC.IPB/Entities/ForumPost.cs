using System;
using Newtonsoft.Json;

namespace BioEngine.BRC.IPB.Entities
{
    public class ForumPost
    {
        public int Id { get; set; }
        
        [JsonProperty("item_id")]
        public int ItemId { get; set; }
        public IpbUser? Author { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; } = "";
        public bool Hidden { get; set; }
        public string Url { get; set; } = "";
    }

    public class PostCreateModel
    {
        public string Post { get; set; } = "";
        public int Author { get; set; }
    }
}
