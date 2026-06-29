using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class TypeCineMap : IEntityTypeConfiguration<TypeCine>
    {
        public void Configure(EntityTypeBuilder<TypeCine> builder)
        {
            builder.ToTable("TYPE_CINE");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(250);
        }
    }
}
