using System.Collections.Generic;
using System.Threading.Tasks;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{

    public interface ISearchService
    {
        Task<IEnumerable<SnippetSearchModel>> SearchAsync(string query);
        Task SaveSnippetAsync(SnippetSearchModel model);
        Task ReindexAsync(List<SnippetSearchModel> searchModels);
    }
}
