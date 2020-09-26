﻿using BioEngine.BRC.Core.Entities.Blocks;
using FluentValidation;
using JetBrains.Annotations;

namespace BioEngine.BRC.Core.Validation
{
    [UsedImplicitly]
    public class TextBlockValidator : AbstractValidator<TextBlock>
    {
        public TextBlockValidator()
        {
            RuleFor(p => p.Data.Text).MinimumLength(250).WithMessage("Текст поста не должен быть меньше 250 символов.");
        }
    }
}
