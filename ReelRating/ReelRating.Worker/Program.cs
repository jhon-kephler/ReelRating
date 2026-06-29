using ReelRating.Infrastructure.DependencyInjection;
using ReelRating.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWorker(builder.Configuration);
builder.Services.AddHostedService<SyncCineJob>();

var host = builder.Build();
host.Run();
