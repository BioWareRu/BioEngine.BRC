using System;
using Microsoft.AspNetCore.WebUtilities;
using Sitko.Core.Storage;

namespace BioEngine.BRC.Core.Extensions
{
    public static class StorageExtensions
    {
        public static Uri ThumbnailUri(this IStorage<BRCStorageConfig> storage, StorageItem storageItem, int width,
            int height)
        {
            var uri = storage.PublicUri(storageItem);
            var uriBuilder = new UriBuilder(uri);
            var query = QueryHelpers.ParseQuery(uri.Query);
            query["width"] = width.ToString();
            query["height"] = height.ToString();
            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;
        }

        public static Uri SmallThumbnailUri(this IStorage<BRCStorageConfig> storage, StorageItem storageItem)
        {
            return storage.ThumbnailUri(storageItem, 100, 100);
        }

        public static Uri MediumThumbnailUri(this IStorage<BRCStorageConfig> storage, StorageItem storageItem)
        {
            return storage.ThumbnailUri(storageItem, 300, 300);
        }

        public static Uri LargeThumbnailUri(this IStorage<BRCStorageConfig> storage, StorageItem storageItem)
        {
            return storage.ThumbnailUri(storageItem, 800, 578);
        }
    }

    public class StorageItemMetadata
    {
        public StorageItemType Type { get; set; } = StorageItemType.File;
        public StorageItemImageMetadata? ImageMetadata { get; set; }
    }

    public class StorageItemImageMetadata
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public enum StorageItemType
    {
        File,
        Image
    }
}
