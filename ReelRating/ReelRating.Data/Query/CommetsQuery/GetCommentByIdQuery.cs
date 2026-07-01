using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CommetsQuery
{
    public interface IGetCommentByIdQuery
    {
        Task<Comments?> GetReviewById(int id);
        Task<Comments?> GetReviewByIdAndCustomerId(int id, int customerId);
        Task<Comments?> GetReviewByIdAndCustomerIdAndCineId(int id, int customerId, int cineId);
    }

    public class GetCommentByIdQuery : IGetCommentByIdQuery
    {
        private readonly IRepository<Comments> _repository;

        public GetCommentByIdQuery(IRepository<Comments> repository)
        {
            _repository = repository;
        }

        public async Task<Comments?> GetReviewById(int id)
        {
            var result = new Comments();
            result = await _repository.GetAsync(x => x.Id == id);
            return result;
        }

        public async Task<Comments?> GetReviewByIdAndCustomerId(int id, int customerId)
        {
            var result = new Comments();
            result = await _repository.GetAsync(x => x.Id == id && x.CustomerId == customerId);
            return result;
        }

        public async Task<Comments?> GetReviewByIdAndCustomerIdAndCineId(int id, int customerId, int cineId)
        {
            var result = new Comments();
            result = await _repository.GetAsync(x => x.Id == id && x.CustomerId == customerId && x.CineId == cineId);
            return result;
        }
    }
}
