using ReelRating.Core.Schema.CustomerNoteSchema.Response;
using ReelRating.Domain.Entities;
using ReelRating.Domain.Repositories;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.ReviewQuery
{
    public interface IGetReviewByNotesQuery { Task<List<CustomerAverageNoteResponse>> GetReviewByNotes(); }

    public class GetReviewByNotesQuery : IGetReviewByNotesQuery
    {
        private readonly IReviewRepository _repository;

        public GetReviewByNotesQuery(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerAverageNoteResponse>> GetReviewByNotes()
        {
            var result = new List<CustomerAverageNoteResponse>();
            result = await _repository.GetAllAsync();
            return result;
        }
    }
}
