using System.Threading.Tasks;

namespace BioEngine.BRC.Admin.Components.RenderService
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync<TModel>(string viewName, TModel model);
    }
}
