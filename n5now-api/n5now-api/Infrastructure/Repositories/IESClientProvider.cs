using Nest;

namespace n5now_api.Infrastructure.Repositories
{
    public interface IESClientProvider
    {
        ElasticClient GetClient();
    }
}
