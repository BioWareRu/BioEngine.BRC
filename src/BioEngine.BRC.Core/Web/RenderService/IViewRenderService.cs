using System.Threading.Tasks;

namespace BioEngine.BRC.Core.Web.RenderService
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync<TModel>(string viewName, TModel model);
    }
}
