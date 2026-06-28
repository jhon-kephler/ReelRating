using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("CUSTOMER");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Nickname).HasColumnName("NICKNAME").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasColumnName("EMAIL").HasMaxLength(200).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique().HasDatabaseName("IDX_CUSTOMER_EMAIL");
            builder.Property(x => x.Password).HasColumnName("PASSWORD").HasMaxLength(200).IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("CREATEDAT").HasColumnType("DATE").HasDefaultValueSql("SYSDATE").IsRequired();
            builder.Property(x => x.Status).HasColumnName("STATUS").HasColumnType("NUMBER(1)").HasDefaultValue(1).IsRequired();
        }
    }
}
