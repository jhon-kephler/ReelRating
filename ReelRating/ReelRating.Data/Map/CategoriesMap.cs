using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class CategoriesMap : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.ToTable("CATEGORIES", schema: "MOVIEDB");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Type_Id).HasColumnName("TYPE_ID");

            builder.HasOne<Type_Cine>().WithMany().HasForeignKey(x => x.Type_Id).HasConstraintName("FK_CATEGORY_TYPE");
        }
    }
}
