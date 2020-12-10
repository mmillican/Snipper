using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Options;
using Snipper.Web.Configuration;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{
    public class SnippetService : DynamoDbService<SnippetFileRecord>
    {
        private readonly ISearchService _searchService;

        public SnippetService(IAmazonDynamoDB dynamoClient,
            ISearchService searchService,
            IOptions<DynamoConfig> dynamoOptions)
        {
            var config = dynamoOptions.Value;
            _searchService = searchService;
            Init(dynamoClient, config.SnippetTableName);
        }

        public Task<IEnumerable<SnippetFileRecord>> GetAllAsync()
        {
            return base.GetAsync();
        }

        public Task<IEnumerable<SnippetFileRecord>> GetByCategory(string slug)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("Category", ScanOperator.Equal, slug)
            };

            return QueryAsync(conditions, "SnippetCategory");
        }

        public override async Task SaveAsync(SnippetFileRecord snippet)
        {
            await base.SaveAsync(snippet);

            var searchModel = new SnippetSearchModel
            {
                Id = snippet.Id,
                Category = snippet.Category,
                SnippetId = snippet.SnippetId,
                Name = snippet.Name,
                Description = snippet.Description,
                FileName = snippet.FileName,
                Language = snippet.Language,
                Content = snippet.Content
            };

            await _searchService.SaveSnippetAsync(searchModel);
        }
    }
}
