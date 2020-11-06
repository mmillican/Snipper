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
        private readonly CategoryService _categoryService;

        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(CategoryService Categorieservice,
            ILogger<CategoriesController> logger)
        {
            _categoryService = Categorieservice;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Snippet>>> GetCategories()
        {
            var categories = await _categoryService.QueryAsync();

            categories = categories.OrderBy(x => x.Name);

            return Ok(categories);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<Snippet>> GetBySlug(string slug)
        {
            var snippet = await _categoryService.GetByIdAsync(slug);
            if (snippet == null)
            {
                return NotFound();
            }

            return Ok(snippet);
        }

        [HttpPost("")]
        public async Task<ActionResult<Snippet>> Create(Category model)
        {
            model.Slug = model.Name.Slugify();

            // TODO: Verify slug doesn't exist

            await _categoryService.SaveAsync(model);

            return CreatedAtAction(nameof(GetBySlug), new { slug = model.Slug }, model);
        }

        [HttpPut("{slug}")]
        public async Task<ActionResult> Update(string slug, Category model)
        {
            var snippet = await _categoryService.GetByIdAsync(slug);
            if (snippet == null)
            {
                return NotFound();
            }

            model.Slug = slug; // Make sure the slug can't be changed

            await _categoryService.SaveAsync(model);

            return NoContent();
        }
    }
}
