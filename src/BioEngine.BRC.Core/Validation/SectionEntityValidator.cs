using BioEngine.BRC.Core.Entities.Abstractions;
using FluentValidation;

namespace BioEngine.BRC.Core.Validation
{
    public sealed class SectionEntityValidator<T> : AbstractValidator<T>
        where T : ISiteEntity, ISectionEntity
    {
        public SectionEntityValidator()
        {
            RuleFor(e => e.SectionIds).NotEmpty();
        }
    }
}
