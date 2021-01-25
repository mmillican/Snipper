using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{
    public interface ISnippetService
    {
        Task<SnippetModel> GetByIdAsync(Guid id);

        Task<IEnumerable<SnippetModel>> GetByCategorySlugAsync(string slug);

        Task<SnippetModel> CreateAsync(SnippetModel model);

        Task UpdateAsync(SnippetModel model);
    }
}
