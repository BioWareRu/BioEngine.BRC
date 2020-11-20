using System;
using FluentValidation;
using Sitko.Core.Storage;

namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("quoteblock", "Цитата")]
    public class QuoteBlock : ContentBlock<QuoteBlockData>
    {
        public override string ToString()
        {
            return $"{Data.Author}: {Data.Text} ({Data.Link})";
        }
    }

    public class QuoteBlockData : ContentBlockData
    {
        public string Text { get; set; } = string.Empty;
        public string? Author { get; set; }
        public string? Link { get; set; }
        public StorageItem? Picture { get; set; }
    }
    
    public class QuoteBlockDataValidator : AbstractValidator<QuoteBlockData>
    {
        public QuoteBlockDataValidator()
        {
            RuleFor(d => d.Text).NotEmpty().WithMessage("Введите текст цитаты");
            RuleFor(p => p.Link).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Link)).WithMessage("Значение должно быть ссылкой");
        }
    }
}
