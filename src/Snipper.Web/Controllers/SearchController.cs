using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snipper.Web.Models;
using Snipper.Web.Services;

namespace Snipper.Web.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ISnippetService _snippetService;

        public SearchController(ISearchService searchService,
            ISnippetService snippetService)
        {
            _searchService = searchService;
            _snippetService = snippetService;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<SnippetSearchModel>>> Search(string query)
        {
            var snippets = await _searchService.SearchAsync(query);

            return Ok(snippets);
        }

        // [HttpGet("re-index")] // TODO: Make a POST
        // public async Task<ActionResult> ReIndex()
        // {
        //     var allSnippets = await _snippetService.GetAllAsync();

        //     var searchModels = allSnippets.Select(snippet =>
        //         new SnippetSearchModel
        //         {
        //             Id = snippet.Id,
        //             Category = snippet.Category,
        //             SnippetId = snippet.SnippetId,
        //             Name = snippet.Name,
        //             Description = snippet.Description,
        //             FileName = snippet.FileName,
        //             Language = snippet.Language,
        //             Content = snippet.Content
        //         })
        //         .ToList();

        //     await _searchService.ReindexAsync(searchModels);

        //     return Ok();
        // }

    }
}
