using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class CommentsMap : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder.ToTable("COMMENTS");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.CustomerId).HasColumnName("CUSTOMER_ID");
            builder.Property(x => x.CineId).HasColumnName("CINE_ID");
            builder.HasIndex(x => x.CineId).HasDatabaseName("IDX_COMMENT_CINE");
            builder.Property(x => x.CommentText).HasColumnName("COMMENT_TEXT").HasMaxLength(2000);
            builder.Property(x => x.Deleted).HasColumnName("DELETED").HasDefaultValue(false);

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).HasConstraintName("FK_COMMENTS_CUSTOMER");
            builder.HasOne<Cine>().WithMany().HasForeignKey(x => x.CineId).HasConstraintName("FK_COMMENTS_CINE");
        }
    }
}
