using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Configurations.Write;

public class BreedConfiguration:IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id).
            HasConversion(id => id.Value, 
                value => BreedId.Create(value));

        builder.Property(t => t.Name)
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
            .IsRequired();
    }
}