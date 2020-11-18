using System.Linq;
using Sitko.Core.Storage;

namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("galleryblock","Галерея")]
    public class GalleryBlock : ContentBlock<GalleryBlockData>
    {
        public override string ToString()
        {
            return $"Галерея: {string.Join(", ", Data.Pictures.Select(p => p.FileName))}";
        }
    }

    public class GalleryBlockData : ContentBlockData
    {
        public StorageItem[] Pictures { get; set; } = new StorageItem[0];
    }
}
