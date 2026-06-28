using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class NotesMap : IEntityTypeConfiguration<Notes>
    {
        public void Configure(EntityTypeBuilder<Notes> builder)
        {
            builder.ToTable("NOTES");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Cine_Id).HasColumnName("CINE_ID");
            builder.Property(x => x.IMDB_Note).HasColumnName("IMDB_NOTE").HasMaxLength(10);
            builder.Property(x => x.TOMMATERS_Note).HasColumnName("TOMMATERS_NOTE").HasMaxLength(10);
            builder.Property(x => x.POPCORNMETER).HasColumnName("POPCORNMETER").HasMaxLength(10);
            builder.Property(x => x.Customer_Notes).HasColumnName("CUSTOMER_NOTES").HasMaxLength(4000);

            builder.HasOne<Cine>().WithMany().HasForeignKey(x => x.Cine_Id).HasConstraintName("FK_NOTES_CINE");
        }
    }
}
