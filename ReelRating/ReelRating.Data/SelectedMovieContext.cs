using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data
{
    public class ReelRatingContext : DbContext
    {
        public ReelRatingContext(DbContextOptions<ReelRatingContext> options) : base(options) { }
    }
}
