using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Command.NotesCommand
{
    public interface IUpdateNotesCommand { Task UpdateNotes(int id, Notes notes); }

    public class UpdateNotesCommand : IUpdateNotesCommand
    {
        private readonly IRepository<Notes> _repository;

        public UpdateNotesCommand(IRepository<Notes> repository)
        {
            _repository = repository;
        }

        public Task UpdateNotes(int id, Notes notes)
        {
            _repository.Update(id, notes);
            return Task.CompletedTask;
        }
    }
}
