using Sitko.Core.Storage;

namespace BioEngine.BRC.Core.Entities.Blocks
{
    [Entity("fileblock", "Файл")]
    public class FileBlock : ContentBlock<FileBlockData>
    {
        public override string ToString()
        {
            return Data.File is null ? "Файл не выбран" : $"Файл: {Data.File.FileName}";
        }
    }

    public class FileBlockData : ContentBlockData
    {
        public StorageItem? File { get; set; }
    }
}
