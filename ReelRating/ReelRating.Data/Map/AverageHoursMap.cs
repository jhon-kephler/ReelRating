using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class AverageHoursMap : IEntityTypeConfiguration<AverageHours>
    {
        public void Configure(EntityTypeBuilder<AverageHours> builder)
        {
            builder.ToTable("AVERAGE_HOURS");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.HoursId).HasColumnName("HOURS_ID");
            builder.Property(x => x.Hours).HasColumnName("HOURS");
            builder.Property(x => x.Mount).HasColumnName("MOUNT");
        }
    }
}
