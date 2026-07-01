using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CommetsQuery
{
    public interface IListCommentsQuery
    {
        Task<List<Comments?>> ListComments(int pageNumber, int pageSize);
        Task<List<Comments?>> ListCommentsById(int id, int pageNumber, int pageSize);
    }

    public class ListCommentsQuery : IListCommentsQuery
    {
        private readonly IRepository<Comments> _repository;

        public ListCommentsQuery(IRepository<Comments> repository)
        {
            _repository = repository;
        }

        public async Task<List<Comments?>> ListCommentsById(int id, int pageNumber, int pageSize)
        {
            var result = new List<Comments?>();
            result = _repository.GetAllByPredicatePagination(x => x.CustomerId == id && x.Deleted == false, pageNumber, pageSize).ToList();
            return result;
        }

        public async Task<List<Comments?>> ListComments(int pageNumber, int pageSize)
        {
            var result = new List<Comments?>();
            result = _repository.GetAllByPredicatePagination(x => x.Deleted == false, pageNumber, pageSize).ToList();
            return result;
        }
    }
}
