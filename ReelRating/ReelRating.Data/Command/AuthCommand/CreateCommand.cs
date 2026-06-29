using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Command.AuthCommand
{
    public interface ICreateCommand { Task<bool> CreateAsync(Customer customer); }

    public class CreateCommand : ICreateCommand
    {
        private IRepository<Customer> _repository;

        public CreateCommand(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(Customer customer)
        {
            _repository.Add(customer);
            return await Task.FromResult(true);
        }
    }
}
