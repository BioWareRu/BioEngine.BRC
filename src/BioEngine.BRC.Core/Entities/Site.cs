using System.ComponentModel.DataAnnotations;
using BioEngine.BRC.Core.Entities.Abstractions;

namespace BioEngine.BRC.Core.Entities
{
    [Entity("site")]
    public class Site : BaseEntity, IBioEntity
    {
        [Required] public string Url { get; set; }

        [Required] public string Title { get; set; }
    }
}
