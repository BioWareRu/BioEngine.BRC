using System;
using Sitko.Core.Search;

namespace BioEngine.BRC.Core.Search
{
    public class BRCSearchModel : BaseSearchModel
    {
        public string AuthorId { get; set; } = string.Empty;
        public Guid[] SiteIds { get; set; } = new Guid[0];
        public Guid[] SectionIds { get; set; } = new Guid[0];
        public string[] Tags { get; set; } = new string[0];

        public BRCSearchModel(string id, string title, string url,
            string content, DateTimeOffset date) : base(id, title, url, content, date)
        {
        }
    }
}
