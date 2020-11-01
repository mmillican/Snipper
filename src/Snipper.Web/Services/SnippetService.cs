using System;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{
    public class SnippetService : DynamoDbService<Snippet>
    {
        public SnippetService()
        {
            var tableName = Environment.GetEnvironmentVariable("SnippetTable");            
            Init(tableName);
        }
    }
}