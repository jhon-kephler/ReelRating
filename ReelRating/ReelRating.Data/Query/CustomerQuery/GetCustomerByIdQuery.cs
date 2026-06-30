using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.CustomerQuery
{
    public interface IGetCustomerByIdQuery { Task<Customer?> GetCustomerById(int id); }

    public class GetCustomerByIdQuery : IGetCustomerByIdQuery
    {
        private readonly IRepository<Customer> _repository;

        public GetCustomerByIdQuery(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            var result = new Customer();
            result = await _repository.GetAsync(x => x.Id == id);
            return result;
            
        }
    }
}
