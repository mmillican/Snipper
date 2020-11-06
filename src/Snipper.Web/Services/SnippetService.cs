using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Options;
using Snipper.Web.Configuration;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{
    public class SnippetService : DynamoDbService<Snippet>
    {
        public SnippetService(IOptions<DynamoConfig> dynamoOptions)
        {
            var config = dynamoOptions.Value;
            Init(config.SnippetTableName);
        }

        public Task<IEnumerable<Snippet>> GetByCategory(string slug)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("Category", ScanOperator.Equal, slug)
            };

            return QueryAsync(conditions, "SnippetCategory");
        }
    }
}
