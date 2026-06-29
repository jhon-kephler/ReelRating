using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReelRating.Domain.Entities;

namespace ReelRating.Data.Map
{
    public class PreferencesMap : IEntityTypeConfiguration<Preferences>
    {
        public void Configure(EntityTypeBuilder<Preferences> builder)
        {
            builder.ToTable("PREFERENCES");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.CustomerId).HasColumnName("CUSTOMER_ID");
            builder.Property(x => x.CategoriesId).HasColumnName("CATEGORIES_ID");
            builder.Property(x => x.NoteOrigin).HasColumnName("NOTE_ORIGIN").HasMaxLength(200);

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).HasConstraintName("FK_PREF_CUSTOMER");
            builder.HasOne<Categories>().WithMany().HasForeignKey(x => x.CategoriesId).HasConstraintName("FK_PREF_CATEGORY");
        }
    }
}
