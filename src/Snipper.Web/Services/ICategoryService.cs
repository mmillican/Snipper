using System.Collections.Generic;
using System.Threading.Tasks;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetAllAsync();

        Task<CategoryModel> GetBySlugAsync(string slug);

        Task<CategoryModel> CreateAsync(CategoryModel model);
        Task UpdateAsync(CategoryModel model);
    }
}
