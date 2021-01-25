using System;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Options;
using Snipper.Web.Configuration;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{

    public class CategoryService : DynamoDbService<CategoryModel>
    {
        public CategoryService(IAmazonDynamoDB dynamoClient,
            IOptions<DynamoConfig> dynamoOptions)
        {
            var config = dynamoOptions.Value;
            Init(dynamoClient, config.CategoryTableName);
        }
    }
}
