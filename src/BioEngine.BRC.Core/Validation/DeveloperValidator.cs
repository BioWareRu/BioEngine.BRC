using BioEngine.BRC.Core.Entities;
using FluentValidation;
using JetBrains.Annotations;

namespace BioEngine.BRC.Core.Validation
{
    [UsedImplicitly]
    public class DeveloperValidator : AbstractValidator<Developer>
    {
        public DeveloperValidator()
        {
            RuleFor(d => d.Title).NotEmpty();
        }
    }
}
