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
        private readonly ISnippetService _snippetService;

        private readonly ILogger<SnippetsController> _logger;

        public SnippetsController(ISnippetService snippetService,
            ILogger<SnippetsController> logger)
        {
            _snippetService = snippetService;
            _logger = logger;
        }

        // [HttpGet("")]
        // public async Task<ActionResult<List<SnippetModel>>> GetSnippets()
        // {
        //     var snippets = await _snippetService.

        //     return Ok(snippets);
        // }

        [HttpGet("/api/categories/{slug}/snippets")]
        public async Task<ActionResult<SnippetModel>> GetByCategory(string slug)
        {
            var snippets = await _snippetService.GetByCategorySlugAsync(slug);
            // TODO: May need to transform the data a bit
            return Ok(snippets);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SnippetModel>> GetById(Guid id)
        {
            var snippet = await _snippetService.GetByIdAsync(id);
            if (snippet == null)
            {
                return NotFound();
            }

            return Ok(snippet);
        }

        [HttpPost("")]
        public async Task<ActionResult<SnippetModel>> Create(SnippetModel model)
        {
            try
            {
                var result = await _snippetService.CreateAsync(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating new snippet");
                return StatusCode(500);
            }

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, SnippetModel model)
        {
            try
            {
                await _snippetService.UpdateAsync(model);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error updating snippet {id}");
                return StatusCode(500);
            }
        }

    //     private async Task SaveSnippet(SnippetModel model)
    //     {
    //         foreach (var file in model.Files)
    //         {
    //             var record = new SnippetFileRecord
    //             {
    //                 Category = model.Category,
    //                 SnippetId = model.Id,
    //                 Name = model.Name,
    //                 Description = model.Description,
    //                 CreatedOn = model.CreatedOn ?? DateTime.UtcNow,
    //                 UpdatedOn = DateTime.UtcNow,

    //                 Id = file.Id == Guid.Empty ? Guid.NewGuid() : file.Id,
    //                 Order = file.Order,
    //                 Language = file.Language,
    //                 FileName = file.FileName,
    //                 Content = file.Content
    //             };

    //             await _snippetService.SaveAsync(record);
    //         }
    //     }

    //     [HttpDelete("{id:guid}")]
    //     public async Task<ActionResult> Delete(string id)
    //     {
    //         var snippet = await _snippetService.GetByIdAsync(id);
    //         if (snippet == null)
    //         {
    //             return NotFound();
    //         }

    //         await _snippetService.DeleteAsync(snippet);

    //         return NoContent();
    //     }
    }

    public class CreateSnippetModel
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
