using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace BioEngine.BRC.Core.Entities
{
    [Table("Tags")]
    [Entity("tag")]
    public class Tag : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
    }

    public class TagValidator : AbstractValidator<Tag>
    {
        public TagValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
