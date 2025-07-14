using NeithDB.Infrastructure.Health;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(sp =>
{
    var opts = sp.GetRequiredService<IOptions<QdrantOptions>>().Value;
    return new QdrantClient(new QdrantClientConfig { Url = opts.Url });
});

builder.Services.AddHealthChecks()
    .AddCheck<QdrantHealthCheck>("qdrant");

builder.Services.AddControllers();
var app = builder.Build();

app.UseRouting();
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
