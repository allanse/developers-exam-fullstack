using Domain.Entities;
using Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Author)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Description)
                   .HasMaxLength(1024);            

            builder.Ignore(p => p.ClassLevelCascadeMode);

            builder.Ignore(p => p.RuleLevelCascadeMode);
            builder.Ignore(p => p.CascadeMode);

        }
    }
}
