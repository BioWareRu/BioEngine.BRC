using System;
using System.Collections.Generic;
using System.Linq;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Blocks;
using Sitko.Core.Repository;

namespace BioEngine.BRC.Admin.Extensions
{
    public static class AdminEntityExtensions
    {
        private static readonly Dictionary<Type, string> _icons = new Dictionary<Type, string>
        {
            {typeof(TextBlock), "fas fa-pen"},
            {typeof(CutBlock), "fas fa-cut"},
            {typeof(QuoteBlock), "fas fa-quote-right"},
            {typeof(PictureBlock), "fas fa-image"},
            {typeof(GalleryBlock), "fas fa-images"},
            {typeof(FileBlock), "fas fa-paperclip"},
            {typeof(YoutubeBlock), "fab fa-youtube"},
            {typeof(TwitterBlock), "fab fa-twitter"},
            {typeof(TwitchBlock), "fab fa-twitch"},
            {typeof(IframeBlock), "fas fa-crop"}
        };

        public static string GetIcon(this IEntity entity)
        {
            return _icons.GetValueOrDefault(entity.GetType(), "");
        }

        public static string GetIcon(this EntityDescriptor descriptor)
        {
            return _icons.GetValueOrDefault(descriptor.Type, "");
        }

        public static List<EntityDescriptor> GetBlocks(this BRCEntitiesRegistrar registrar)
        {
            var blocks = registrar.Blocks;
            return _icons.Keys.Select(type => blocks.First(b => b.Type == type)).ToList();
        }
    }
}
