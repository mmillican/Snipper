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
        public async Task<ActionResult<Snippet>> Create(Snippet model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;

            await _snippetService.SaveAsync(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
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
    }
}
