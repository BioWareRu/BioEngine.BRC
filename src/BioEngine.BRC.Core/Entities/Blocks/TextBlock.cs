using FluentValidation;

namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("textblock", "Текст")]
    public class TextBlock : ContentBlock<TextBlockData>
    {
        public override string ToString()
        {
            return Data.Text;
        }
    }

    public class TextBlockData : ContentBlockData
    {
        public string Text { get; set; } = "";
    }
    
    public class TextBlockDataValidator : AbstractValidator<TextBlockData>
    {
        public TextBlockDataValidator()
        {
            RuleFor(p => p.Text).NotEmpty().WithMessage("Введите текст");
        }
    }
}
