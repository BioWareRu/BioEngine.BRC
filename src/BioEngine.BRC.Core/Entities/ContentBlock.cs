using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Entities.Abstractions;
using Newtonsoft.Json;

namespace BioEngine.BRC.Core.Entities
{
    [Table("ContentBlocks")]
    public abstract class ContentBlock : BaseEntity, ITypedEntity
    {
        [Required] public Guid ContentId { get; set; }
        [Required] public string Type { get; set; } = string.Empty;
        [Required] public int Position { get; set; }
        [NotMapped] public abstract string? TypeTitle { get; set; }

        public abstract object GetData();
        public abstract void SetData(object data);

        public override string ToString()
        {
            return GetType().Name;
        }
    }

    public abstract class ContentBlock<T> : ContentBlock, ITypedEntity<T> where T : ContentBlockData, new()

    {
        [Column(TypeName = "jsonb")] public T Data { get; set; } = new T();

        public override object GetData()
        {
            return Data;
        }

        public override void SetData(object data)
        {
            try
            {
                var typedData = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data));
                if (typedData != null)
                {
                    Data = typedData;
                }
                else
                {
                    throw new Exception($"Can't assign empty data to type {typeof(T)}");
                }
            }
            catch (JsonException)
            {
                throw new Exception($"Can't convert object {data} to type {typeof(T)}");
            }
        }
    }

    public abstract class ContentBlockData : ITypedData
    {
    }
}
