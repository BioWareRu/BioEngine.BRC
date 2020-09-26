using System;
using BioEngine.BRC.Core.Entities;

namespace BioEngine.BRC.Core.Publishing
{
    public abstract class BasePublishRecord : BaseSiteEntity
    {
        public Guid ContentId { get; set; }
        public string Type { get; set; }
    }
}
