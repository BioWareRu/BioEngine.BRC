using BioEngine.BRC.Core.Entities;
using FluentValidation;

namespace BioEngine.BRC.Core.Validation
{
    public sealed class SectionValidator : AbstractValidator<Section>
    {
        public SectionValidator()
        {
            RuleFor(e => e.Title).NotEmpty();
            RuleFor(e => e.Title).NotEmpty();
            RuleFor(e => e.Url).NotEmpty();
        }
    }
}
