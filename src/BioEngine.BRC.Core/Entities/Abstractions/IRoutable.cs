using Sitko.Core.Repository;

namespace BioEngine.BRC.Core.Entities.Abstractions
{
    public interface IRoutable : IEntity
    {
        string PublicRouteName { get; set; }
        string Url { get; set; }
    }
}
