using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Snipper.Web.Models;
using Snipper.Web.Services;

namespace Snipper.Web.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService,
            ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<CategoryModel>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();

            categories = categories.OrderBy(x => x.Name);

            return Ok(categories);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<CategoryModel>> GetBySlug(string slug)
        {
            var snippet = await _categoryService.GetBySlugAsync(slug);
            if (snippet == null)
            {
                return NotFound();
            }

            return Ok(snippet);
        }

        [HttpPost("")]
        public async Task<ActionResult<CategoryModel>> Create(CategoryModel model)
        {
            model.Slug = model.Name.Slugify();

            await _categoryService.CreateAsync(model);

            return CreatedAtAction(nameof(GetBySlug), new { slug = model.Slug }, model);
        }

        [HttpPut("{slug}")]
        public async Task<ActionResult> Update(string slug, CategoryModel model)
        {
            model.Slug = slug; // Make sure the slug can't be changed

            try
            {
                await _categoryService.UpdateAsync(model);
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error updating category '{model.Slug}'");
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
