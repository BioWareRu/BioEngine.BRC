using System;
using System.Linq;
using System.Text.Json.Serialization;
using FluentValidation;

namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("youtubeblock", "Youtube")]
    public class YoutubeBlock : ContentBlock<YoutubeBlockData>
    {
        public override string ToString()
        {
            return $"Youtube: {Data.YoutubeId}";
        }
    }

    public class YoutubeBlockData : ContentBlockData
    {
        public string YoutubeId { get; set; } = "";

        [JsonIgnore]
        public string? YoutubeLink
        {
            get
            {
                return string.IsNullOrEmpty(YoutubeId) ? null : $"https://www.youtube.com/watch?v={YoutubeId}";
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && (value.Contains("http://") || value.Contains("https://")))
                {
                    var uri = new Uri(value);

                    var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);

                    YoutubeId = queryParams.ContainsKey("v") ? queryParams["v"][0] : uri.Segments.Last();
                }
                else
                {
                    YoutubeId = string.Empty;
                }
            }
        }

        [JsonIgnore]
        public string? EmbedUrl =>
            string.IsNullOrEmpty(YoutubeId) ? null : $"https://www.youtube.com/embed/{YoutubeId}";
    }

    public class YoutubeBlockDataValidator : AbstractValidator<YoutubeBlockData>
    {
        public YoutubeBlockDataValidator()
        {
            RuleFor(d => d.YoutubeLink).NotEmpty().WithMessage("Укажите ссылку на видео");
        }
    }
}
