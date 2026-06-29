using Microsoft.EntityFrameworkCore;
using ReelRating.Domain.Entities;
using ReelRating.Domain.Repositories;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReelRating.Data.Repositories
{
    public class CineRepository : Repository<Cine>, ICineRepository
    {
        public CineRepository(DbContext dbContext) : base(dbContext) { }

        public async Task<List<int?>> GetCineYearsAsync(int pageNumber, int pageSize)
        {
            return await _dbSet
                .Select(c => c.Year)
                .Distinct()
                .OrderBy(y => y)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Cine?> GetCineByNameAsync(string cine)
        {
            return await _dbSet
                            .Where(c => c.Name == cine)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<Cine>> ListCineByYearAsync(int year, int pageNumber, int pageSize)
        {
            return await _dbSet
                .Where(c => c.Year == year)
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Cine>> ListCineByIdsAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return new List<Cine>();

            return await _dbSet
                .Where(c => ids.Contains(c.Id))
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}
