using Microsoft.EntityFrameworkCore;
using ReelRating.Domain.Entities;
using ReelRating.Domain.Repositories;
using ReelRating.Core.Schema.CustomerNoteSchema.Response;

namespace ReelRating.Data.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(DbContext dbContext) : base(dbContext) { }

        public async Task<List<CustomerAverageNoteResponse>> GetAllAsync()
        {
            return await _dbSet
                            .Where(x => x.Deleted != true && x.Note.HasValue)
                            .GroupBy(x => x.CineId)
                            .Select(x => new CustomerAverageNoteResponse
                            {
                                CineId = x.Key,
                                Average = x.Average(r => r.Note!.Value)
                            })
                            .ToListAsync();
        }
    }
}
