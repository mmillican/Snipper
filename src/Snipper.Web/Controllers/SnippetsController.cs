using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Snipper.Web.Models;
using Snipper.Web.Services;

namespace Snipper.Web.Controllers
{
    [ApiController]
    [Route("api/snippets")]
    public class SnippetsController : ControllerBase
    {
        private readonly SnippetService _snippetService;

        private readonly ILogger<SnippetsController> _logger;

        public SnippetsController(SnippetService snippetService,
            ILogger<SnippetsController> logger)
        {
            _snippetService = snippetService;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Snippet>>> GetSnippets()
        {
            var snippets = await _snippetService.QueryAsync();

            return Ok(snippets);
        }

        [HttpGet("/api/categories/{slug}/snippets")]
        public async Task<ActionResult<Snippet>> GetByCategory(string slug)
        {
            var snippets = await _snippetService.GetByCategory(slug);

            return Ok(snippets);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Snippet>> GetById(string id)
        {
            var snippet = await _snippetService.GetByIdAsync(id);
            if (snippet == null)
            {
                return NotFound();
            }

            return Ok(snippet);
        }

        [HttpPost("")]
        public async Task<ActionResult<Snippet>> Create(CreateSnippetModel model)
        {
            var snippet = new Snippet
            {
                Id = Guid.NewGuid().ToString(),
                Category = model.Category,
                Name = model.Name,
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            await _snippetService.SaveAsync(snippet);

            return CreatedAtAction(nameof(GetById), new { id = snippet.Id }, snippet);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(string id, Snippet model)
        {
            var snippet = await _snippetService.GetByIdAsync(id);
            if (snippet == null)
            {
                return NotFound();
            }

            model.Id = id; // Make sure the ID can't change
            model.UpdatedOn = DateTime.UtcNow;

            await _snippetService.SaveAsync(model);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(string id)
        {
            var snippet = await _snippetService.GetByIdAsync(id);
            if (snippet == null)
            {
                return NotFound();
            }

            await _snippetService.DeleteAsync(snippet);

            return NoContent();
        }
    }

    public class CreateSnippetModel
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
