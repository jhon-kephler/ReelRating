using Microsoft.Extensions.Logging;
using ReelRating.Application.Services.CustomerNotesServices.Interfaces;
using ReelRating.Data.Command.NotesCommand;
using ReelRating.Data.Query.NotesQuery;
using ReelRating.Data.Query.ReviewQuery;
using ReelRating.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.CustomerNotesServices
{
    public class ManageCustomerNotesService : IManageCustomerNotesService
    {
        private readonly IGetReviewByNotesQuery _getReviewByNotesQuery;
        private readonly IListNotesQuery _listNotesQuery;
        private readonly IUpdateNotesCommand _updateNotesCommand;
        private readonly ILogger<ManageCustomerNotesService> _logger;

        public ManageCustomerNotesService(IGetReviewByNotesQuery getReviewByNotesQuery, IListNotesQuery listNotesQuery, IUpdateNotesCommand updateNotesCommand, ILogger<ManageCustomerNotesService> logger)
        {
            _getReviewByNotesQuery = getReviewByNotesQuery;
            _listNotesQuery = listNotesQuery;
            _updateNotesCommand = updateNotesCommand;
            _logger = logger;
        }

        public async Task UpdateCustomerNotes()
        {
            try
            {
                _logger.LogInformation("Iniciando atualização das notas dos clientes.");

                var averages = await _getReviewByNotesQuery.GetReviewByNotes();

                if (!averages.Any())
                {
                    _logger.LogInformation("Nenhuma avaliação encontrada para atualização.");
                    return;
                }

                var notes = await _listNotesQuery.ListNotes();

                foreach (var average in averages)
                {
                    var note = notes.FirstOrDefault(x => x.CineId == average.CineId);

                    if (note == null)
                    {
                        _logger.LogWarning(
                            "Registro de Notes não encontrado para o filme {CineId}.",
                            average.CineId);

                        continue;
                    }
                    
                    var customerAverage = Math.Round(average.Average, 1, MidpointRounding.AwayFromZero);

                    note.CustomerNotes = customerAverage.ToString("0.0");

                    await _updateNotesCommand.UpdateNotes(note.Id, note);

                    _logger.LogInformation(
                        "Filme {CineId} atualizado com média {Media}.",
                        average.CineId,
                        customerAverage);
                }

                _logger.LogInformation("Notas dos clientes atualizadas com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar notas dos clientes.");
                throw;
            }
        }
    }
}
