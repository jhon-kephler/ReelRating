using Microsoft.EntityFrameworkCore;
using ReelRating.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data
{
    public class ReelRatingContext : DbContext
    {
        public ReelRatingContext(DbContextOptions<ReelRatingContext> options) : base(options) { }

        public DbSet<AverageHours> Average_Hours { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Cine> Cine { get; set; }
        public DbSet<CineCategories> Cine_Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAverageHours> Customer_Average_Hours { get; set; }
        public DbSet<CustomerWhatch> Customer_Whatch { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Preferences> Preferences { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<TypeCine> Type_Cine { get; set; }
        public DbSet<WhatchIn> WhatchIn { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("MOVIEDB");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReelRatingContext).Assembly);
        }
    }
}
