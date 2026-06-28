using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class Customer_Average_HoursMap : IEntityTypeConfiguration<Customer_Average_Hours>
    {
        public void Configure(EntityTypeBuilder<Customer_Average_Hours> builder)
        {
            builder.ToTable("Customer_Average_Hours");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Customer_Id).HasColumnName("Customer_Id");
            builder.Property(x => x.Hours).HasColumnName("Hours");
            builder.Property(x => x.Field).HasColumnName("Field").HasMaxLength(100);
        }
    }
}
