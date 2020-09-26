using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities.Abstractions;

namespace BioEngine.BRC.Core.Web
{
    public interface IContentRender
    {
        Task<string> RenderHtmlAsync(IContentEntity contentEntity, Entities.Site site,
            ContentEntityViewMode mode = ContentEntityViewMode.List);
    }
}
