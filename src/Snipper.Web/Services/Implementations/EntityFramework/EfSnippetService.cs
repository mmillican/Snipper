using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Snipper.Data;
using Snipper.Web.Entities;
using Snipper.Web.Models;

namespace Snipper.Web.Services.Implmentations.EntityFramework
{
    public class EfSnippetService : ISnippetService
    {
        private readonly SnipperDbContext _dbContext;
        private readonly IMapper _mapper;

        public EfSnippetService(SnipperDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SnippetModel> GetByIdAsync(Guid id)
        {
            var snippet = await _dbContext.Snippets
                .Include(x => x.Files)
                .SingleOrDefaultAsync(x => x.Id == id);

            var model = _mapper.Map<SnippetModel>(snippet);

            return model;
        }

        public async Task<IEnumerable<SnippetModel>> GetByCategorySlugAsync(string slug)
        {
            var snippets = await _dbContext.Snippets
                .Include(x => x.Category)
                .Where(x => x.Category.Slug == slug)
                .ProjectTo<SnippetModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return snippets;
        }

        public async Task<SnippetModel> CreateAsync(SnippetModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var snippet = new Snippet
            {
                CategorySlug = model.Category,
                Name = model.Name,
                Description = model.Description,
                // TODO: Username
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            foreach(var fileModel in model.Files)
            {
                var file = new SnippetFile
                {
                    Id = Guid.NewGuid(),
                    Snippet = snippet,
                    Order = fileModel.Order,
                    Language = fileModel.Language,
                    FileName = fileModel.FileName,
                    Content = fileModel.Content
                };

                snippet.Files.Add(file);
            }

            _dbContext.Snippets.Add(snippet);
            await _dbContext.SaveChangesAsync();

            var result = await GetByIdAsync(snippet.Id);
            return result;
        }

        public async Task UpdateAsync(SnippetModel model)
        {
            var snippet = await _dbContext.Snippets.FindAsync(model.Id);
            if (snippet == null)
            {
                throw new KeyNotFoundException("Snippet not found");
            }

            // Update the "meta data" on the snippet
            try
            {
                snippet.CategorySlug = model.Category;
                snippet.Name = model.Name;
                snippet.Description = model.Description;
                snippet.UpdatedOn = DateTime.UtcNow;

                _dbContext.Snippets.Update(snippet);

                var fileIds = model.Files.Select(x => x.Id);

                var existingFiles = await _dbContext.SnippetFiles
                    .Where(x => x.SnippetId == model.Id)
                    .ToListAsync();
                var existingFileIds = existingFiles.Select(x => x.Id);

                // Determine which files should be removed
                var filesToDelete = existingFiles
                    .Where(x => !fileIds.Contains(x.Id));

                _dbContext.SnippetFiles.RemoveRange(filesToDelete);

                // Determine which files to add (new files will have empty GUID)
                var filesToAdd = model.Files.Where(x => x.Id == Guid.Empty);
                foreach(var addFileModel in filesToAdd)
                {
                    var file = new SnippetFile
                    {
                        Id = Guid.NewGuid(),
                        SnippetId = snippet.Id,
                        Order = addFileModel.Order,
                        Language = addFileModel.Language,
                        FileName = addFileModel.FileName,
                        Content = addFileModel.Content
                    };

                    _dbContext.SnippetFiles.Add(file);
                }

                // And update existing files
                var filesToUpdate = model.Files
                    .Where(x => existingFileIds.Contains(x.Id));
                foreach(var fileModel in filesToUpdate)
                {
                    var file = existingFiles.FirstOrDefault(x => x.Id == fileModel.Id);
                    file.Order = fileModel.Order;
                    file.Language = fileModel.Language;
                    file.FileName = fileModel.FileName;
                    file.Content = fileModel.Content;

                    _dbContext.SnippetFiles.Update(file);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
