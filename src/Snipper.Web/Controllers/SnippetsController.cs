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
            var snippetRecords = await _snippetService.GetByCategory(slug);

            var snippetGroups = snippetRecords.GroupBy(x => x.SnippetId);

            var snippets = new List<Snippet>();

            foreach(var grp in snippetGroups)
            {
                var snippetGrpRecord = snippetRecords
                    .FirstOrDefault(x => x.SnippetId == grp.Key);

                var snip = new Snippet
                {
                    Id = snippetGrpRecord.SnippetId,
                    Category = snippetGrpRecord.Category,
                    Name = snippetGrpRecord.Name,
                    Description = snippetGrpRecord.Description,
                    CreatedOn = snippetGrpRecord.CreatedOn,
                    UpdatedOn = snippetGrpRecord.UpdatedOn
                };

                snip.Files = grp.Select(x => new SnippetFile
                {
                    Id = x.Id,
                    Order = x.Order,
                    FileName = x.FileName,
                    Language = x.Language,
                    Content = x.Content
                }).ToList();

                snippets.Add(snip);
            }

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
            model.Id = Guid.NewGuid();
            await SaveSnippet(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, Snippet model)
        {
            model.Id = id;
            await SaveSnippet(model);

            return NoContent();
        }

        private async Task SaveSnippet(Snippet model)
        {
            foreach (var file in model.Files)
            {
                var record = new SnippetFileRecord
                {
                    Category = model.Category,
                    SnippetId = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    CreatedOn = model.CreatedOn ?? DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,

                    Id = file.Id == Guid.Empty ? Guid.NewGuid() : file.Id,
                    Order = file.Order,
                    Language = file.Language,
                    FileName = file.FileName,
                    Content = file.Content
                };

                await _snippetService.SaveAsync(record);
            }
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
