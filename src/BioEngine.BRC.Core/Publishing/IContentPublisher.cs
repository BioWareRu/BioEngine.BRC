using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;

namespace BioEngine.BRC.Core.Publishing
{
    public interface IContentPublisher<TConfig> where TConfig : IContentPublisherConfig
    {
        Task<bool> PublishAsync(IContentItem entity, TConfig config, bool needUpdate, Site site, bool allSites = false);
        Task<bool> DeleteAsync(IContentItem entity, TConfig config, Site site, bool allSites = false);
    }
}
