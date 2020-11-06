using System;
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
    }
}
