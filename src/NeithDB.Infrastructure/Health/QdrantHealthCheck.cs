using Microsoft.Extensions.Diagnostics.HealthChecks;
using Qdrant.Client;

namespace NeithDB.Infrastructure.Health
{
    public class QdrantHealthCheck : IHealthCheck
    {
        private readonly QdrantClient _client;
        public QdrantHealthCheck(QdrantClient client) => _client = client;

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var colls = await _client.GetCollectionsAsync();
                return HealthCheckResult.Healthy("Qdrant disponível");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Qdrant indisponível", ex);
            }
        }
    }
}
