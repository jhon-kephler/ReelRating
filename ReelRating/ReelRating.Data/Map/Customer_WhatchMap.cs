using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class Customer_WhatchMap : IEntityTypeConfiguration<Customer_Whatch>
    {
        public void Configure(EntityTypeBuilder<Customer_Whatch> builder)
        {
            builder.ToTable("CUSTOMER_WHATCH");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Customer_Id).HasColumnName("CUSTOMER_ID");
            builder.Property(x => x.Qtt_Access).HasColumnName("QTT_ACCESS").HasDefaultValue(0);
            builder.Property(x => x.Whatch_Id).HasColumnName("WHATCH_ID");

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.Customer_Id).HasConstraintName("FK_CW_CUSTOMER");
            builder.HasOne<WhatchIn>().WithMany().HasForeignKey(x => x.Whatch_Id).HasConstraintName("FK_CW_WHATCH");
        }
    }
}
