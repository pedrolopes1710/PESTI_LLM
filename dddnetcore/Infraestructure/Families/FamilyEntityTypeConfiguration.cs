using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDSample1.Domain.Families;

namespace DDDSample1.Infrastructure.Families
{
    internal class FamilyEntityTypeConfiguration : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.HasKey(f => f.Id);
            
            builder.Property(f => f.Id)
                .HasConversion(
                    id => id.AsString(),  
                    str => new FamilyId(str));  

            builder.Property(f => f.Description).IsRequired();
            builder.Property(f => f.Active).IsRequired();
        }
    }
}