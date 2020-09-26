using System;
using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Entities.Abstractions;

namespace BioEngine.BRC.Core.Entities
{
    [Table("StorageItems")]
    [Entity("storageitem")]
    public class StorageItem : Sitko.Core.Storage.StorageItem, IBioEntity
    {
        public object GetId()
        {
            return Id.ToString();
        }

        public Guid Id { get; set; }

        public static StorageItem FromCore(Sitko.Core.Storage.StorageItem item)
        {
            return new StorageItem
            {
                Path = item.Path, FileName = item.FileName, FilePath = item.FilePath, FileSize = item.FileSize
            };
        }

        public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.UtcNow;
    }
}
