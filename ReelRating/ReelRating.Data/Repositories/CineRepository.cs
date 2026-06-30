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

        public async Task<Cine> GetCineByIdAsync(int cineId)
        {
            var cine = await _dbSet
                .FirstOrDefaultAsync(c => c.Id == cineId);

            if (cine == null)
            {
                throw new Exception($"Cine with ID '{cineId}' not found.");
            }
            return cine;
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

        public async Task<(List<Cine> Items, int Total)> SearchCineAsync(int? categoryId, int? year, int pageNumber,int pageSize)
        {
            var query = _dbSet.AsQueryable();

            if (year.HasValue)
                query = query.Where(c => c.Year == year.Value);

            if (categoryId.HasValue)
                query = query.Where(c =>
                    c.CineCategories.Any(cc => cc.CategoriesId == categoryId.Value));

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }
    }
}
