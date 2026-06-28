using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class WhatchInMap : IEntityTypeConfiguration<WhatchIn>
    {
        public void Configure(EntityTypeBuilder<WhatchIn> builder)
        {
            builder.ToTable("WHATCHIN");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Region).HasColumnName("REGION").HasMaxLength(100);
            builder.Property(x => x.Available).HasColumnName("AVAILABLE").HasDefaultValue(1).IsRequired();
        }
    }
}
