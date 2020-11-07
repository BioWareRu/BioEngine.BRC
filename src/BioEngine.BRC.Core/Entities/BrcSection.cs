using BioEngine.BRC.Core.Entities.Abstractions;
using Sitko.Core.Storage;

namespace BioEngine.BRC.Core.Entities
{
    public abstract class BrcSection<TData> : Section<TData> where TData : BrcSectionData, new()
    {
    }

    public abstract class BrcSectionData : ITypedData
    {
        public virtual StorageItem? HeaderPicture { get; set; }
        public virtual string? Hashtag { get; set; }
    }
}
