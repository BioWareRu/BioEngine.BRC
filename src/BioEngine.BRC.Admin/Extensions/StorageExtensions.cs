using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Core;
using BioEngine.BRC.Core.Extensions;
using Sitko.Core.Storage;
using SixLabors.ImageSharp;

namespace BioEngine.BRC.Admin.Extensions
{
    public static class StorageExtensions
    {
        private static readonly string[] _imageExtensions = {".jpg", ".jpeg", ".png"};

        public static Task<StorageItem> ProcessAndUploadFileAsync(this IStorage<BRCStorageConfig> storage, Stream file,
            string fileName,
            string path)
        {
            var metaData = new StorageItemMetadata();
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            if (_imageExtensions.Contains(extension))
            {
                file.Position = 0;
                metaData.Type = StorageItemType.Image;
                using Image image = Image.Load(file);
                metaData.ImageMetadata = new StorageItemImageMetadata {Height = image.Height, Width = image.Width};
                file.Position = 0;
            }

            return storage.SaveFileAsync(file, fileName, path, metaData);
        }

        public static (string path, string name)[] GetPathParts(string path)
        {
            var parts = new List<(string path, string name)> {("/", "Начало")};
            if (!string.IsNullOrEmpty(path) && path != "/")
            {
                string[] directories = path.Split(Path.DirectorySeparatorChar);

                string previousEntry = string.Empty;
                foreach (string dir in directories)
                {
                    string newEntry = previousEntry + Path.DirectorySeparatorChar + dir;
                    if (!string.IsNullOrEmpty(newEntry))
                    {
                        if (!newEntry.Equals(Convert.ToString(Path.DirectorySeparatorChar),
                            StringComparison.OrdinalIgnoreCase))
                        {
                            parts.Add((newEntry.Trim(Path.DirectorySeparatorChar),
                                dir.Replace("/", "")));
                            previousEntry = newEntry;
                        }
                    }
                }
            }

            return parts.ToArray();
        }
    }
}
