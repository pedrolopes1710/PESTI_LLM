using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDSample1.Domain.Categories;

namespace DDDSample1.Infrastructure.Categories
{
    internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(),
                    guid => new CategoryId(guid));

            builder.Property(b => b.Description).IsRequired();
            builder.Property(b => b.Active).IsRequired();
        }
    }
}