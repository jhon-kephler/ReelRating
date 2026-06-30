using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using ReelRating.Core.Schema.CustomerNoteSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<List<CustomerAverageNoteResponse>> GetAllAsync();
    }
}
