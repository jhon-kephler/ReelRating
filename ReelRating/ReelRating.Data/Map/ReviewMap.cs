using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class ReviewMap : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("REVIEW");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.CustomerId).HasColumnName("CUSTOMER_ID");
            builder.Property(x => x.CineId).HasColumnName("CINE_ID");
            builder.Property(x => x.TypeId).HasColumnName("TYPE_ID");
            builder.Property(x => x.REVIEW).HasColumnName("REVIEW").HasMaxLength(2000);
            builder.Property(x => x.Note).HasColumnName("NOTE");
            builder.Property(x => x.Deleted).HasColumnName("DELETED").HasDefaultValue(false);

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).HasConstraintName("FK_REVIEW_CUSTOMER");
            builder.HasOne<Cine>().WithMany().HasForeignKey(x => x.CineId).HasConstraintName("FK_REVIEW_CINE");
            builder.HasOne<TypeCine>().WithMany().HasForeignKey(x => x.TypeId).HasConstraintName("FK_REVIEW_TYPE");
        }
    }
}
