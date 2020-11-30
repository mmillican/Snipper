using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Options;
using Snipper.Web.Configuration;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{
    public class SnippetSearchService : DynamoDbService<SnippetSearchModel>
    {
        public SnippetSearchService(IOptions<DynamoConfig> dynamoOptions)
        {
            var config = dynamoOptions.Value;
            Init(config.SnippetTableName);
        }

        public Task<IEnumerable<SnippetSearchModel>> Search(string query)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("Content", ScanOperator.Contains, query)
            };

            return QueryAsync(conditions, "SnippetSearch");
        }
    }
}
