using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Snipper.Web.Services
{
    public abstract class DynamoDbService<TModel> where TModel : class, new()
    {
        private IDynamoDBContext _ddbContext;

        public IDynamoDBContext DbContext => _ddbContext;
        // public DynamoDbService()
        // {
        //     var tableName = Environment.GetEnvironmentVariable("SnippetTable");
        //     Init(tableName);
        // }

        // public DynamoDbService(IAmazonDynamoDB ddbClient, string tableName)
        // {
        //     Init(ddbClient, tableName);
        // }

        protected void Init(string tableName)
        {
            Init(new AmazonDynamoDBClient(), tableName);
        }

        protected void Init(IAmazonDynamoDB client, string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception($"Missing table name for {typeof(TModel).Name}");
            }

            var config = new DynamoDBContextConfig
            {
                Conversion = DynamoDBEntryConversion.V2
            };

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(TModel)] =
                new Amazon.Util.TypeMapping(typeof(TModel), tableName);

            _ddbContext = new DynamoDBContext(client, config);

        }

        public Task<TModel> GetByIdAsync(string id)
            => _ddbContext.LoadAsync<TModel>(id);


        public async Task<IEnumerable<TModel>> QueryAsync(
            List<ScanCondition> conditions = null, string indexName = null)
        {
            var query = _ddbContext.ScanAsync<TModel>(conditions,
                new DynamoDBOperationConfig
                {
                    IndexName = indexName
                });

            var result = await query.GetNextSetAsync();
            return result;

            // var test = await _ddbClient.QueryAsync(queryRequest);
            // test.items
        }

        public async Task<IEnumerable<TModel>> GetAsync()
        {
            var search = _ddbContext.ScanAsync<TModel>(null);
            var page = await search.GetNextSetAsync();  // Not sure if this will return all the records?
            return page;
        }

        public Task SaveAsync(TModel model)
            => _ddbContext.SaveAsync(model);

        public Task DeleteAsync(TModel model)
            => _ddbContext.DeleteAsync(model);
    }
}
