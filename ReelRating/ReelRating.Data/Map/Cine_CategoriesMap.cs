using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class Cine_CategoriesMap : IEntityTypeConfiguration<Cine_Categories>
    {
        public void Configure(EntityTypeBuilder<Cine_Categories> builder)
        {
            builder.ToTable("CINE_CATEGORIES");
            builder.HasKey(x => new { x.Cine_Id, x.Categories_Id });
            builder.Property(x => x.Cine_Id).HasColumnName("CINE_ID");
            builder.Property(x => x.Categories_Id).HasColumnName("CATEGORIES_ID");

            builder.HasOne<Cine>().WithMany().HasForeignKey(x => x.Cine_Id).HasConstraintName("FK_CC_CINE");
            builder.HasOne<Categories>().WithMany().HasForeignKey(x => x.Categories_Id).HasConstraintName("FK_CC_CATEGORY");
        }
    }
}
