using System.Threading.Tasks;

namespace CodeWithMukesh
{
    public interface IViewRenderService
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}