using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Snipper.Web.Models;

namespace Snipper.Web.Services
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(this IServiceCollection services,
            IConfiguration configuration)
        {
            var url = configuration["search:elastic:url"];
            var indexName = configuration["search:elastic:indexName"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(indexName);

            settings.DefaultMappingFor<SnippetSearchModel>(x => x);

            var client = new ElasticClient(settings);

            services.AddSingleton(client);
            services.AddTransient<ISearchService, SearchService>();

            // TODO: Probably don't want to run this here...
            CreateIndex(client, indexName);
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<SnippetSearchModel>(x => x.AutoMap()));
        }
    }
}
