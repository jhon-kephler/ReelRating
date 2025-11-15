using SelectedMovie.Infrastructure;
using SelectedMovie.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddWorker(builder.Configuration);

var host = builder.Build();
host.Run();
