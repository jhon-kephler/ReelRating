using ReelRating.Infrastructure.DependencyInjection;
using ReelRating.Worker.Jobs;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWorker(builder.Configuration);
builder.Services.AddHostedService<SyncCineJob>();
builder.Services.AddHostedService<CustomerNotesJob>();

var host = builder.Build();
host.Run();
