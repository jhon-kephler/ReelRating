using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class CineCategoriesMap : IEntityTypeConfiguration<CineCategories>
    {
        public void Configure(EntityTypeBuilder<CineCategories> builder)
        {
            builder.ToTable("CINE_CATEGORIES");
            builder.HasKey(x => new { x.CineId, x.CategoriesId });
            builder.Property(x => x.CineId).HasColumnName("CINE_ID");
            builder.Property(x => x.CategoriesId).HasColumnName("CATEGORIES_ID");

            builder.HasOne<Cine>().WithMany().HasForeignKey(x => x.CineId).HasConstraintName("FK_CC_CINE");
            builder.HasOne<Categories>().WithMany().HasForeignKey(x => x.CategoriesId).HasConstraintName("FK_CC_CATEGORY");
        }
    }
}
