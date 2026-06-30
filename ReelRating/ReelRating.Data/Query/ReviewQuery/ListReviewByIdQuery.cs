using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.ReviewQuery
{
    public interface IListReviewByIdQuery { Task<List<Review?>> ListReviewById(int id, int pageNumber, int pageSize); }

    public class ListReviewByIdQuery : IListReviewByIdQuery
    {
        private readonly IRepository<Review> _repository;

        public ListReviewByIdQuery(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task<List<Review?>> ListReviewById(int id,int pageNumber, int pageSize)
        {
            var result = new List<Review?>();
            result = _repository.GetAllByPredicatePagination(x => x.CustomerId == id && x.Deleted == false, pageNumber, pageSize).ToList();
            return result;
        }
    }
}
