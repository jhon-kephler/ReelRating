using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class CineMap : IEntityTypeConfiguration<Cine>
    {
        public void Configure(EntityTypeBuilder<Cine> builder)
        {
            builder.ToTable("CINE");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(200).IsRequired();
            builder.HasIndex(x => x.Name).HasDatabaseName("IDX_CINE_NAME");
            builder.Property(x => x.Year).HasColumnName("YEAR").HasColumnType("NUMBER(4)");
            builder.Property(x => x.Month).HasColumnName("MONTH").HasColumnType("NUMBER(2)");
            builder.Property(x => x.WhatchId).HasColumnName("WHATCH_ID");
            builder.Property(x => x.TypeId).HasColumnName("TYPE_ID");
            builder.Property(x => x.URLPoster).HasColumnName("URL_POSTER").HasMaxLength(500);

            builder.HasOne<WhatchIn>().WithMany().HasForeignKey(x => x.WhatchId).HasConstraintName("FK_CINE_WHATCH");
            builder.HasOne<TypeCine>().WithMany().HasForeignKey(x => x.TypeId).HasConstraintName("FK_CINE_TYPE");
        }
    }
}
