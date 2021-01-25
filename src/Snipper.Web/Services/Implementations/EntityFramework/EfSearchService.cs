using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snipper.Data;
using Snipper.Web.Models;

namespace Snipper.Web.Services.Implmentations.EntityFramework
{
    public class EfSearchService : ISearchService
    {
        private readonly SnipperDbContext _dbContext;

        public EfSearchService(SnipperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SnippetSearchModel>> SearchAsync(string query)
        {
            var snippets = await _dbContext.SnippetFiles
                .Include(x => x.Snippet)
                .ThenInclude(x => x.Category)
                .Where(x => x.Snippet.Name.Contains(query)
                    || x.Snippet.Description.Contains(query)
                    || x.Content.Contains(query)
                )
                .ToListAsync();

            var results = snippets.Select(x => new SnippetSearchModel
            {
                Category = x.Snippet.CategorySlug,
                Id = x.Id,
                SnippetId = x.SnippetId,
                Name = x.Snippet.Name,
                Description = x.Snippet.Description,
                FileName = x.FileName,
                Language = x.Language,
                Content = x.Content
            });

            return results;
        }

        public Task SaveSnippetAsync(SnippetSearchModel model)
        {
            throw new NotImplementedException();
        }

        public Task ReindexAsync(List<SnippetSearchModel> models)
        {
            throw new NotImplementedException();
        }
    }
}
