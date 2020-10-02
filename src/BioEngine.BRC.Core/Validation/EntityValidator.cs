using BioEngine.BRC.Core.Entities.Abstractions;
using FluentValidation;

namespace BioEngine.BRC.Core.Validation
{
    public class EntityValidator : AbstractValidator<IBioEntity>
    {
        public EntityValidator()
        {
            RuleFor(e => e.DateAdded).NotEmpty();
            RuleFor(e => e.DateUpdated).NotEmpty();
        }
    }
}
