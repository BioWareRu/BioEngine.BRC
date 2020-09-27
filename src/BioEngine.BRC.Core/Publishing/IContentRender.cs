using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities.Abstractions;

namespace BioEngine.BRC.Core.Publishing
{
    public interface IContentRender
    {
        Task<string> RenderHtmlAsync(IContentEntity contentEntity, Entities.Site site,
            ContentEntityViewMode mode = ContentEntityViewMode.List);
    }
}
