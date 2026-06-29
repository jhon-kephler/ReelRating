using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class FavoritesMap : IEntityTypeConfiguration<Favorites>
    {
        public void Configure(EntityTypeBuilder<Favorites> builder)
        {
            builder.ToTable("FAVORITES");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.CustomerId).HasColumnName("CUSTOMER_ID");
            builder.Property(x => x.CineId).HasColumnName("CINE_ID");
            builder.Property(x => x.Deleted).HasColumnName("DELETED").HasDefaultValue(false);

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).HasConstraintName("FK_FAVORITES_CUSTOMER");
            builder.HasOne<Cine>().WithMany().HasForeignKey(x => x.CineId).HasConstraintName("FK_FAVORITES_CINE");
        }
    }
}
