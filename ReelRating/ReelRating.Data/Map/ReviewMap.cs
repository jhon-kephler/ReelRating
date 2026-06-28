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
            builder.Property(x => x.Customer_Id).HasColumnName("CUSTOMER_ID");
            builder.Property(x => x.Cine_Id).HasColumnName("CINE_ID");
            builder.Property(x => x.Categories_Id).HasColumnName("CATEGORIES_ID");
            builder.Property(x => x.Type_Id).HasColumnName("TYPE_ID");
            builder.Property(x => x.Status_Id).HasColumnName("STATUS_ID");
            builder.HasIndex(x => x.Status_Id).HasDatabaseName("IDX_REVIEW_STATUS");
            builder.Property(x => x.REVIEW).HasColumnName("REVIEW").HasMaxLength(2000);
            builder.Property(x => x.Note).HasColumnName("NOTE");
            builder.Property(x => x.Deleted).HasColumnName("DELETED").HasDefaultValue(false);

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.Customer_Id).HasConstraintName("FK_REVIEW_CUSTOMER");
            builder.HasOne<Cine>().WithMany().HasForeignKey(x => x.Cine_Id).HasConstraintName("FK_REVIEW_CINE");
            builder.HasOne<Categories>().WithMany().HasForeignKey(x => x.Categories_Id).HasConstraintName("FK_REVIEW_CATEGORY");
            builder.HasOne<Type_Cine>().WithMany().HasForeignKey(x => x.Type_Id).HasConstraintName("FK_REVIEW_TYPE");
            builder.HasOne<Status>().WithMany().HasForeignKey(x => x.Status_Id).HasConstraintName("FK_REVIEW_STATUS");
        }
    }
}
