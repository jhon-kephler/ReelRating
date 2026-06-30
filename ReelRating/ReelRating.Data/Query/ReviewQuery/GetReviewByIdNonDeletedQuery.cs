using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.ReviewQuery
{
    public interface IGetReviewByIdNonDeletedQuery { Task<Review?> GetReviewByIdNonDeleted(int id); }

    public class GetReviewByIdNonDeletedQuery : IGetReviewByIdNonDeletedQuery
    {
        private readonly IRepository<Review> _repository;

        public GetReviewByIdNonDeletedQuery(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task<Review?> GetReviewByIdNonDeleted(int id)
        {
            var result = new Review();
            result = await _repository.GetAsync(x => x.Id == id && x.Deleted == false);
            return result;
        }
    }
}
