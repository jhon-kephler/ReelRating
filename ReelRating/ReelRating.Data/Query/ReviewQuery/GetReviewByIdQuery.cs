using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.ReviewQuery
{
    public interface IGetReviewByIdQuery { Task<Review?> GetReviewById(int id); }

    public class GetReviewByIdQuery : IGetReviewByIdQuery
    {
        private readonly IRepository<Review> _repository;

        public GetReviewByIdQuery(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task<Review?> GetReviewById(int id)
        {
            var result = new Review();
            result = await _repository.GetAsync(x => x.Id == id);
            return result;
        }
    }
}
