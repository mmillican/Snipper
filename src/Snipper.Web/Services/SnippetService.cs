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
        public SnippetService(IAmazonDynamoDB dynamoClient,
            IOptions<DynamoConfig> dynamoOptions)
        {
            var config = dynamoOptions.Value;
            Init(dynamoClient, config.SnippetTableName);
        }

        public Task<IEnumerable<SnippetFileRecord>> GetByCategory(string slug)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("Category", ScanOperator.Equal, slug)
            };

            return QueryAsync(conditions, "SnippetCategory");
        }
    }
}
