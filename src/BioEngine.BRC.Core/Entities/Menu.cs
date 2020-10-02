using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioEngine.BRC.Core.Entities
{
    [Entity("menu")]
    public class Menu : BaseSiteEntity
    {
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "jsonb")]
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
    }

    public class MenuItem
    {
        public string Label { get; set; } = "";
        public string Url { get; set; } = "";
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
    }
}
