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
            builder.Property(x => x.Whatch_Id).HasColumnName("WHATCH_ID");
            builder.Property(x => x.Type_Id).HasColumnName("TYPE_ID");
            builder.Property(x => x.URL_Poster).HasColumnName("URL_POSTER").HasMaxLength(500);

            builder.HasOne<WhatchIn>().WithMany().HasForeignKey(x => x.Whatch_Id).HasConstraintName("FK_CINE_WHATCH");
            builder.HasOne<Type_Cine>().WithMany().HasForeignKey(x => x.Type_Id).HasConstraintName("FK_CINE_TYPE");
        }
    }
}
