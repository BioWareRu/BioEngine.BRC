using Sitko.Core.Storage;

namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("pictureblock", "Картинка")]
    public class PictureBlock : ContentBlock<PictureBlockData>
    {
        public override string ToString()
        {
            return Data.Picture is null ? "Картинка не выбрана" : $"Картинка: {Data.Picture.FileName}";
        }
    }

    public class PictureBlockData : ContentBlockData
    {
        public StorageItem? Picture { get; set; }
        public string? Url { get; set; }
    }
}
