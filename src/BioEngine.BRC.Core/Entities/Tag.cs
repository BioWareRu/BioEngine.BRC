using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioEngine.BRC.Core.Entities
{
    [Table("Tags")]
    [Entity("tag")]
    public class Tag : BaseEntity
    {
        [Required]
        public string Title { get; set; }
    }
}
