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
            builder.Property(x => x.CineId).HasColumnName("CINE_ID");
            builder.Property(x => x.IMDBNote).HasColumnName("IMDB_NOTE").HasMaxLength(10);
            builder.Property(x => x.TOMMATERSNote).HasColumnName("TOMMATERS_NOTE").HasMaxLength(10);
            builder.Property(x => x.POPCORNMETER).HasColumnName("POPCORNMETER").HasMaxLength(10);
            builder.Property(x => x.Customer_Notes).HasColumnName("CUSTOMER_NOTES").HasMaxLength(4000);

            builder.HasOne<Cine>().WithMany().HasForeignKey(x => x.CineId).HasConstraintName("FK_NOTES_CINE");
        }
    }
}
