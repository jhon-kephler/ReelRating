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
            builder.Property(x => x.Customer_Id).HasColumnName("CUSTOMER_ID");
            builder.Property(x => x.Cine_Id).HasColumnName("CINE_ID");
            builder.HasIndex(x => x.Cine_Id).HasDatabaseName("IDX_COMMENT_CINE");
            builder.Property(x => x.Comment_Text).HasColumnName("COMMENT_TEXT").HasMaxLength(2000);
            builder.Property(x => x.Deleted).HasColumnName("DELETED").HasDefaultValue(false);

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.Customer_Id).HasConstraintName("FK_COMMENTS_CUSTOMER");
            builder.HasOne<Cine>().WithMany().HasForeignKey(x => x.Cine_Id).HasConstraintName("FK_COMMENTS_CINE");
        }
    }
}
