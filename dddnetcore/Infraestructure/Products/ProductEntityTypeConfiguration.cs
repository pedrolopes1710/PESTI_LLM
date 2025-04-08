using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDSample1.Domain.Products;
using DDDSample1.Domain.Categories;

namespace DDDSample1.Infrastructure.Products
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new ProductId(guid));

            builder.Property(p => p.CategoryId)
                .HasConversion(
                    catId => catId.AsGuid(), 
                    guid => new CategoryId(guid)); 

            builder.HasKey(b => b.Id);
        }
    }
}