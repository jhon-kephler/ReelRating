using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class Customer_WhatchMap : IEntityTypeConfiguration<CustomerWhatch>
    {
        public void Configure(EntityTypeBuilder<CustomerWhatch> builder)
        {
            builder.ToTable("CUSTOMER_WHATCH");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.CustomerId).HasColumnName("CUSTOMER_ID");
            builder.Property(x => x.QttAccess).HasColumnName("QTT_ACCESS").HasDefaultValue(0);
            builder.Property(x => x.WhatchId).HasColumnName("WHATCH_ID");

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).HasConstraintName("FK_CW_CUSTOMER");
            builder.HasOne<WhatchIn>().WithMany().HasForeignKey(x => x.WhatchId).HasConstraintName("FK_CW_WHATCH");
        }
    }
}
