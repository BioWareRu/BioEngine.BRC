using BioEngine.BRC.Core.Entities.Abstractions;

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
