using ReelRating.Application.Services.CustomerNotesServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Worker.Jobs
{
    public class CustomerNotesJob : BackgroundService
    {
        private readonly ILogger<CustomerNotesJob> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public CustomerNotesJob(ILogger<CustomerNotesJob> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CustomerNotesJob iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var service = scope.ServiceProvider.GetRequiredService<IManageCustomerNotesService>();

                    await service.UpdateCustomerNotes();

                    _logger.LogInformation("Atualização das notas concluída em {Date}.", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Erro ao executar CustomerNotesJob.");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}