using System;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{
    public class CategoryService : DynamoDbService<Category>
    {
        public CategoryService()
        {
            var tableName = Environment.GetEnvironmentVariable("CategoryTable");
            Init(tableName);
        }
    }
}