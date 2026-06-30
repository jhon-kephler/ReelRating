using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.NotesQuery
{
    public interface IListNotesQuery { Task<IEnumerable<Notes>> ListNotes(); }

    public class ListNotesQuery : IListNotesQuery
    {
        private readonly IRepository<Notes> _repository;

        public ListNotesQuery(IRepository<Notes> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Notes>> ListNotes()
        {
            var result = new List<Notes>();
            result = _repository.GetAll().ToList();
            return result;
        }
    }
}
