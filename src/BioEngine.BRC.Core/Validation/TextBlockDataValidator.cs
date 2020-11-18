using BioEngine.BRC.Core.Entities.Blocks;
using FluentValidation;
using JetBrains.Annotations;

namespace BioEngine.BRC.Core.Validation
{
    [UsedImplicitly]
    public class TextBlockDataValidator : AbstractValidator<TextBlockData>
    {
        public TextBlockDataValidator()
        {
            RuleFor(p => p.Text).MinimumLength(250).WithMessage("Текст поста не должен быть меньше 250 символов.");
        }
    }
}
