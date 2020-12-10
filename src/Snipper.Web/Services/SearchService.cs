using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nest;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<SnippetSearchModel>> SearchAsync(string query);
        Task SaveSnippetAsync(SnippetSearchModel model);
        Task ReindexAsync(List<SnippetSearchModel> searchModels);
    }

    public class SearchService : ISearchService
    {
        private readonly ElasticClient _elasticClient;
        private readonly ILogger<SearchService> _logger;

        public SearchService(ElasticClient elasticClient,
            ILogger<SearchService> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task<IEnumerable<SnippetSearchModel>> SearchAsync(
            string query)
        {
            var searchResponse = await _elasticClient.SearchAsync<SnippetSearchModel>(x => x
                .Query(q => q
                    .Match(m => m
                        .Field(x => x.Content).Query(query)
                    )
                )
            );

            _logger.LogInformation($"Found {searchResponse.Total} results for '{query}'");

            return searchResponse.Documents;
        }

        public async Task SaveSnippetAsync(SnippetSearchModel model)
        {
            try
            {
                await _elasticClient.IndexDocumentAsync(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error indexing snippet '{0}:{1}'", model.Name, model.FileName);
                throw new Exception("Error saving snippet to search index", ex);
            }
        }

        public async Task ReindexAsync(List<SnippetSearchModel> searchModels)
        {

            await _elasticClient.DeleteByQueryAsync<SnippetSearchModel>(x => x.MatchAll());

            await _elasticClient.IndexManyAsync<SnippetSearchModel>(searchModels);
        }
    }
}
