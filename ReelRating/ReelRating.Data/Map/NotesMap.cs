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
            builder.Property(x => x.TMDBNote).HasColumnName("TMDB_NOTE").HasMaxLength(10);
            builder.Property(x => x.CustomerNotes).HasColumnName("CUSTOMER_NOTES").HasMaxLength(4000);

            builder.HasOne<Cine>().WithMany().HasForeignKey(x => x.CineId).HasConstraintName("FK_NOTES_CINE");
        }
    }
}
