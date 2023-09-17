using Nest;

namespace n5now_api.Infrastructure.Repositories
{
    public class ESClientProvider : IESClientProvider
    {
        private readonly IConfiguration _configuration;
        private ElasticClient _client;

        public ESClientProvider(IConfiguration configuration) => _configuration = configuration;

        public ElasticClient GetClient()
        {
            if (_client != null)
                return _client;
            InitClient();
            return _client;
        }

        private void InitClient()
        {
            var node = new Uri(_configuration["ESUrl"]);
            _client = new ElasticClient(new ConnectionSettings(node).DefaultIndex("demo"));
        }
    }
}
