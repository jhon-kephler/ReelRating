using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Query.AuthQuery
{
    public interface IGetCustomerQuery 
    { 
        Task<Customer> GetByUserAsync(string user);
        Task<Customer?> GetByEmailAsync(string email);
        Task<Customer?> GetByNickNameAsync(string name);
    }

    public class GetCustomerQuery : IGetCustomerQuery
    {
        private IRepository<Customer> _repository;

        public GetCustomerQuery(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer?> GetByUserAsync(string user)
        {
            var result = new Customer();
            result = await _repository.GetAsync(x => x.Nickname == user);
            return result;
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            var result = new Customer();
            result = await _repository.GetAsync(x => x.Email == email);
            return result;
        }

        public async Task<Customer?> GetByNickNameAsync(string name)
        {
            var result = new Customer();
            result = await _repository.GetAsync(x => x.Nickname == name);
            return result;
        }
    }
}
