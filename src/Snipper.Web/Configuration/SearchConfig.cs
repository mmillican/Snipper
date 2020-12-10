namespace Snipper.Web.Configuration
{
    public class SearchConfig
    {
        public ElasticSearchConfig Elastic { get; set; }
    }

    public class ElasticSearchConfig
    {
        public string Url { get; set; }
        public string IndexName { get; set; }
    }
}
