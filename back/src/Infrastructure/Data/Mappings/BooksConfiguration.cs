using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class BooksConfiguration : IEntityTypeConfiguration<Books>
    {
        public void Configure(EntityTypeBuilder<Books> builder)
        {
            builder.ToTable("Book");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Author)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Description)
                   .HasMaxLength(1024);

            builder.Property(b => b.CreatedDate)
                   .IsRequired();

            builder.Property(b => b.LastUpdatedDate);

            builder.Ignore(p => p.ClassLevelCascadeMode);

            builder.Ignore(p => p.RuleLevelCascadeMode);
            builder.Ignore(p => p.CascadeMode);

        }
    }
}
