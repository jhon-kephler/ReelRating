using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Repositories
{
    public interface ICineRepository : IRepository<Cine>
    {
        Task<List<int?>> GetCineYearsAsync(int pageNumber, int pageSize);
        Task<Cine> GetCineByNameAsync(string cine);
        Task<List<Cine>> ListCineByYearAsync(int year);
        Task<List<Cine>> ListCineByIdsAsync(List<int> ids);
    }
}
