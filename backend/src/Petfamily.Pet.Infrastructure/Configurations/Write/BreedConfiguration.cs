using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Pet.Domain.Volunteers.IDs;
using PetFamily.Pet.Domain.Volunteers.Species;
using PetFamily.Core;

namespace Petfamily.Pet.Infrastructure.Configurations.Write;

public class BreedConfiguration:IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breed");
        
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