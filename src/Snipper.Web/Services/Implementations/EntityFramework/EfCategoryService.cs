using System.Collections.Generic;
using System.Threading.Tasks;
using Snipper.Data;
using Snipper.Web.Models;
using AutoMapper;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using Snipper.Web.Entities;

namespace Snipper.Web.Services.Implmentations.EntityFramework
{
    public class EfCategoryService : ICategoryService
    {
        private readonly SnipperDbContext _dbContext;
        private readonly IMapper _mapper;

        public EfCategoryService(SnipperDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            var categories = await _dbContext.Categories
                .OrderBy(x => x.Name)
                .ProjectTo<CategoryModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return categories;
        }

        public async Task<CategoryModel> GetBySlugAsync(string slug)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Slug == slug);

            var model = _mapper.Map<CategoryModel>(category);
            return model;
        }

        public async Task<CategoryModel> CreateAsync(CategoryModel model)
        {
            try
            {
                var category = new Category
                {
                    Slug = model.Slug.ToLower(),
                    Name = model.Name
                };

                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<CategoryModel>(category);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(CategoryModel model)
        {
            // Want to get the entity not the model
            var category = await _dbContext.Categories
                .SingleOrDefaultAsync(x => x.Slug == model.Slug);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            try
            {
                category.Name = model.Name;

                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
