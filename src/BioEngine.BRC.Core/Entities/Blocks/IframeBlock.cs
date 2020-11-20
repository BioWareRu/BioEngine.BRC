using System;
using FluentValidation;

namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("iframeblock", "Iframe")]
    public class IframeBlock : ContentBlock<IframeBlockData>
    {
        public override string ToString()
        {
            return $"Frame: {Data.Src}";
        }
    }

    public class IframeBlockData : ContentBlockData
    {
        public string Src { get; set; } = "";
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
    }

    public class IframeBlockDataValidator : AbstractValidator<IframeBlockData>
    {
        public IframeBlockDataValidator()
        {
            RuleFor(p => p.Src).NotEmpty().WithMessage("Укажите ссылку")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Значение должно быть ссылкой");
        }
    }
}
