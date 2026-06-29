using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class CustomerAverageHoursMap : IEntityTypeConfiguration<CustomerAverageHours>
    {
        public void Configure(EntityTypeBuilder<CustomerAverageHours> builder)
        {
            builder.ToTable("Customer_Average_Hours");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.CustomerId).HasColumnName("Customer_Id");
            builder.Property(x => x.Hours).HasColumnName("Hours");
            builder.Property(x => x.Field).HasColumnName("Field").HasMaxLength(100);
        }
    }
}
