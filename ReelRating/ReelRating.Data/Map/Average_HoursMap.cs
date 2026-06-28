using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class Average_HoursMap : IEntityTypeConfiguration<Average_Hours>
    {
        public void Configure(EntityTypeBuilder<Average_Hours> builder)
        {
            builder.ToTable("AVERAGE_HOURS");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Hours_Id).HasColumnName("HOURS_ID");
            builder.Property(x => x.Hours).HasColumnName("HOURS");
            builder.Property(x => x.Mount).HasColumnName("MOUNT");
        }
    }
}
